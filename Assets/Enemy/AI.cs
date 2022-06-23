using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;
    private Transform    m_PlayerTransform;
    private Latarka      m_Latarka;
    private DetectDoor   m_DoorDetector;
    
    [Header("Pan Bialy Navigation")]

    public Vector3 m_WalkPoint;

    [Header("Pan Bialy Properties")]

    public float m_TimeBetweenAtacks;
    public float m_SightRange, m_AttackRange, m_WalkRange, m_RunRange;
    private bool m_AlreadyAtacked;

    public int m_BehaviourUpdateTickRate = 40;

    public float m_DoorOpenTime;
    [Header("FOV of Pan Bialy")]

    [Range(0.0f,180.0f)]
    public float m_FOVEulers;
    public uint  m_DebugFovRays = 16;
    private bool m_IsWalking = false;
    private bool m_WalkingEnabled = true;
    private DoorController m_DoorInFront;
    static public bool m_Enabled = true;
    Vector3 AtEnemyY(Vector3 vec)
    {
        return new Vector3(vec.x, transform.position.y, vec.z);
    }
    bool IsPointInFOV(Vector3 point)
    {
        return (Vector3.Angle(transform.forward, (AtEnemyY(point) - transform.position).normalized) < m_FOVEulers/2.0f);
    }
    bool IsDirInFOV(Vector3 dir)
    {
        return (Vector3.Angle(transform.forward, dir) < m_FOVEulers/2.0f);
    }
    bool IsPointVisible(Vector3 point)
    {
        if(!IsPointInFOV(point))
            return false;

        if(Vector3.Distance(transform.position, AtEnemyY(point)) > m_SightRange)
            return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (AtEnemyY(point) - transform.position).normalized, out hit, m_SightRange))
            if(!hit.collider.CompareTag("Player"))
                return false;

        return true;
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

    #region AI Core
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

        bool playerInSightRange  = (distanceToPlayer < m_SightRange);
        bool playerInAttackRange = (distanceToPlayer < m_AttackRange);
        bool playerInWalkRange = (distanceToPlayer < m_WalkRange);
        bool playerInRunRange = (distanceToPlayer < m_RunRange);
        bool playerAway = (distanceToPlayer > 10.0f);

        GameObject door = m_DoorDetector.CollidedWithDoor();

        if(door != null)
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

        if((IsPointVisible(m_PlayerTransform.position) && playerInSightRange) || playerInAttackRange || IsFlashlightVisible() || (TunnelMoving.PlayerMoves() && playerInWalkRange) || (TunnelMoving.isRunning && playerInRunRange))
        {
            if(playerInAttackRange)
                AttackPlayer();
            else
                ChasePlayer();
        }
        else if(DistanceToWalkPoint() < 1.0f && m_IsWalking)
            SearchWalkPoint();
        else if(!m_IsWalking)
            SearchWalkPoint();
        if(playerAway)
            m_NavMeshAgent.speed = 4.0f;
    }
    #endregion

    #region AI Actions
    private void SearchWalkPoint()
    {
        int randomNodeIndex = Random.Range(0, Nodes.nodes.Length);

        Vector3 nodePos = AtEnemyY(Nodes.nodes[randomNodeIndex].position);

        SetWalkPoint(nodePos);
    }

    private void ChasePlayer()
    {
        m_NavMeshAgent.speed = 3.0f;
        SetWalkPoint(m_PlayerTransform.position);
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
    
    private bool IsFlashlightVisible()
    {
        foreach(Vector3 point in Latarka.m_LatarkaPoints)
        {
            if(IsPointVisible(point))
                return true;
        }

        return false;
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

    #region Debug Methods
    private void OnDrawGizmosSelected()
    {
        Transform playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_SightRange);

        Gizmos.DrawLine(transform.position, transform.position + m_SightRange * (playerTarget.position - transform.position).normalized);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (playerTarget.position - transform.position).normalized, out hit, m_SightRange))
            Gizmos.DrawSphere(hit.point, 0.3f);

        float fov = m_FOVEulers / 180.0f;

        Vector3 fovBoundL = new Vector3(Mathf.Sin(Mathf.PI*(-fov/2.0f + transform.eulerAngles.y / 180.0f)), 0.0f, Mathf.Cos(Mathf.PI*(-fov/2.0f + transform.eulerAngles.y / 180.0f)));
        Vector3 fovBoundR = new Vector3(Mathf.Sin(Mathf.PI*( fov/2.0f + transform.eulerAngles.y / 180.0f)), 0.0f, Mathf.Cos(Mathf.PI*( fov/2.0f + transform.eulerAngles.y / 180.0f)));

        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.25f);

        Gizmos.DrawLine(transform.position, transform.position + m_SightRange * fovBoundL);
        Gizmos.DrawLine(transform.position, transform.position + m_SightRange * fovBoundR);

        float rays = (float)m_DebugFovRays;

        for(float i = 0.0f; i <= 1.0f; i+= 1.0f/rays)
        {
            float deltaAngleL = ( fov/2.0f) + (transform.eulerAngles.y / 180.0f);
            float deltaAngleR = (-fov/2.0f) + (transform.eulerAngles.y / 180.0f);

            Vector3 dir = new Vector3(Mathf.Sin(Mathf.PI*Mathf.Lerp(deltaAngleL, deltaAngleR, i)), 0.0f, Mathf.Cos(Mathf.PI*Mathf.Lerp(deltaAngleL, deltaAngleR, i)));

            Gizmos.DrawLine(transform.position, transform.position + m_SightRange * dir);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(m_WalkPoint, 0.3f);
        Gizmos.DrawLine(transform.position, m_WalkPoint);
    }
    #endregion
}