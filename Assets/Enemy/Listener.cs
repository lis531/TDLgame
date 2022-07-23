using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Listener : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;
    private Transform    m_PlayerTransform;
    private Transform m_PlayerTransformLast;
    private Latarka      m_Latarka;
    private DetectDoor   m_DoorDetector;
    
    [Header("Pan Bialy Navigation")]

    public Vector3 m_WalkPoint;

    [Header("Pan Bialy Properties")]

    public float m_TimeBetweenAtacks;
    public float m_SightRange, m_AttackRange, m_WalkRange, m_RunRange;
    private bool m_AlreadyAtacked;

    public int m_BehaviourUpdateTickRate;

    public float m_DoorOpenTime;
    [Header("FOV of Pan Bialy")]
    private bool m_IsWalking = false;
    private bool m_WalkingEnabled = true;
    static public bool Chase;
    private DoorController m_DoorInFront;
    static public bool m_Enabled = true;
    Vector3 AtEnemyY(Vector3 vec)
    {
        return new Vector3(vec.x, transform.position.y, vec.z);
    }

    #region Player Related Methods
    Vector3 DirectionToPlayer()
    {
        return (AtEnemyY(m_PlayerTransform.position) - transform.position).normalized;
    }
    float DistanceToPlayer()
    {
        return Vector3.Distance(AtEnemyY(m_PlayerTransform.position), transform.position);
    }
    #endregion

    #region Walk Point Related Methods
    void SetWalkPoint(Vector3 point)
    {
        m_WalkPoint = point;
        m_IsWalking = true;
        m_NavMeshAgent.SetDestination(m_WalkPoint);
    }
    Vector3 DirectionToWalkPoint()
    {
        return (AtEnemyY(m_WalkPoint) - transform.position).normalized;
    }
    float DistanceToWalkPoint()
    {
        return Vector3.Distance(AtEnemyY(m_WalkPoint), transform.position);
    }
    #endregion

    #region Core Core
    private void Awake()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_NavMeshAgent    = GetComponent<NavMeshAgent>();
        m_DoorDetector    = GetComponent<DetectDoor>();
        m_Latarka         = m_PlayerTransform.GetComponent<Latarka>();

        InvokeRepeating("UpdateBehaviour", 0.0f, 1.0f / (float)m_BehaviourUpdateTickRate);
    }
    void UpdateBehaviour()
    {
        if(gameObject.activeInHierarchy != m_Enabled)
        {
            gameObject.SetActive(m_Enabled);
            SetWalkPoint(transform.position);
        }

        if(!m_WalkingEnabled || !m_Enabled || DevConsole.m_IsOpen) 
        {
            m_NavMeshAgent.Warp(transform.position);
            return;
        }

        float distanceToPlayer = DistanceToPlayer();

        bool playerInAttackRange = (distanceToPlayer < m_AttackRange);
        bool playerInWalkRange = (distanceToPlayer < m_WalkRange);
        bool playerInRunRange = (distanceToPlayer < m_RunRange);

        GameObject door = m_DoorDetector.CollidedWithDoor();

        if(door != null && !Chase)
        {
            m_DoorInFront = door.GetComponent<DoorController>();

            if(m_DoorInFront != null)
            {
                if(!m_DoorInFront.IsOpen())
                {
                    DisableWalking();

                    Invoke("EnableWalking", m_DoorOpenTime + 0.5f);
                    Invoke("OpenDoor", m_DoorOpenTime);

                    return;
                }
            }
        }

        if(playerInAttackRange || (TunnelMoving.PlayerMoves() && playerInWalkRange) || (TunnelMoving.isRunning && playerInRunRange))
        {
            if(playerInAttackRange)
                AttackPlayer();
            else
                m_NavMeshAgent.speed = 3.0f;
                m_PlayerTransformLast = m_PlayerTransform;
                Chase = true;
                ChasePlayer();
        }
        else if(DistanceToWalkPoint() < 1.0f && m_IsWalking)
        {
            SearchWalkPoint();
        }
        else if(!m_IsWalking)
        {
            SearchWalkPoint();
        }
    }
    #endregion

    #region Core Actions
    private void SearchWalkPoint()
    {
        Chase = false;

        int randomNodeIndex = Random.Range(0, Nodes.nodes.Length);

        Vector3 nodePos = AtEnemyY(Nodes.nodes[randomNodeIndex].position);

        SetWalkPoint(nodePos);
    }
    private void ChasePlayer()
    {
        SetWalkPoint(m_PlayerTransformLast.position);
    }

    private void AttackPlayer()
    {
        m_NavMeshAgent.speed = 0.0f;
        ChasePlayer();

        if (!m_AlreadyAtacked)
        {
            m_AlreadyAtacked = true;

            Invoke("ResetAttack", m_TimeBetweenAtacks);

            Health.health -= 20;
            StartCoroutine(Bleeding());
        }
    }
    IEnumerator Bleeding()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Health.health -= 2;
        }
    }
    private void ResetAttack()
    {   
        m_AlreadyAtacked = false;
    }

    private void OpenDoor()
    {
        Transform door = m_DoorInFront.transform;

        if(Vector3.Dot(transform.forward, door.right) < 0.0f)
            transform.position = door.position + (door.right*m_DoorDetector.m_DetectRange);
        else
            transform.position = door.position - (door.right*m_DoorDetector.m_DetectRange);

        transform.LookAt(AtEnemyY(door.position));

        m_DoorInFront.ForceOpen();
        m_DoorInFront = null;
    }

    public void EnableWalking()
    {
        m_WalkingEnabled = true;
        SetWalkPoint(m_WalkPoint);
    }
    public void DisableWalking()
    {
        m_WalkingEnabled = false;
    }
    #endregion
}