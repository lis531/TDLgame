using UnityEngine;

public class Latarka : MonoBehaviour
{
    public GameObject m_Latarka0;
    public GameObject m_Latarka1;

    Transform m_LatarkaTransform;
    Transform m_CameraTransform;
    
    public float m_LatarkaRange = 10f;
    public float m_LatarkaAngles = 45.0f;

    static public Vector3[] m_LatarkaPoints;
     
    private Vector3[] m_LatarkaPointDirs;

    public LayerMask m_LatarkaLayer;

    [Header("Tego nie zmieniaj w Runtimie!:")]

    public int latarkaUpdateTickRate = 30;
    public int latarkaSubSteps = 1;

    void Start()
    {
        if(m_Latarka0 == null)
            Debug.LogError("Latarka.Start() - latarka na Player jest null");

        if(m_Latarka1 == null)
            Debug.LogError("Latarka.Start() - latarka1 na Player jest null");

        m_LatarkaTransform = m_Latarka0.transform;
        m_CameraTransform = transform.GetChild(0).transform;

        m_Latarka0.SetActive(false);
        m_Latarka1.SetActive(false);

        m_LatarkaPoints = new Vector3[9 + (9 * (latarkaSubSteps-1))];

        Vector3 fovCenter  = new Vector3(0.0f, 0.0f, 1.0f);

        float pi = Mathf.PI;
        float halfFov = m_LatarkaAngles / 180.0f / 2.0f;

        Vector3 fovBoundL  = new Vector3(Mathf.Sin(pi *-halfFov)       , 0.0f                         , Mathf.Cos(pi *-halfFov)      );
        Vector3 fovBoundR  = new Vector3(Mathf.Sin(pi * halfFov)       , 0.0f                         , Mathf.Cos(pi * halfFov)      );
        Vector3 fovBoundU  = new Vector3(0.0f                          , Mathf.Sin(pi*-halfFov)       , Mathf.Cos(pi *-halfFov)      );
        Vector3 fovBoundB  = new Vector3(0.0f                          , Mathf.Sin(pi* halfFov)       , Mathf.Cos(pi * halfFov)      );
        Vector3 fovBoundUL = new Vector3(Mathf.Sin(pi *-(halfFov*0.7f)), Mathf.Sin(pi*(halfFov*0.7f)) , Mathf.Cos(pi*-(halfFov*0.7f)));
        Vector3 fovBoundUR = new Vector3(Mathf.Sin(pi * (halfFov*0.7f)), Mathf.Sin(pi*(halfFov*0.7f)) , Mathf.Cos(pi*-(halfFov*0.7f)));
        Vector3 fovBoundBL = new Vector3(Mathf.Sin(pi *-(halfFov*0.7f)), Mathf.Sin(pi*(-halfFov*0.7f)), Mathf.Cos(pi*-(halfFov*0.7f)));
        Vector3 fovBoundBR = new Vector3(Mathf.Sin(pi * (halfFov*0.7f)), Mathf.Sin(pi*(-halfFov*0.7f)), Mathf.Cos(pi*-(halfFov*0.7f)));

        m_LatarkaPointDirs = new Vector3[]{fovBoundL, fovBoundR, fovBoundU, fovBoundB, fovCenter, fovBoundUL, fovBoundUR, fovBoundBL, fovBoundBR};

        float dt = 1.0f / (float)latarkaUpdateTickRate;

        InvokeRepeating("UpdateLightPoints", 0.0f, dt);
    }

    void UpdateLightPoints()
    {
        if(!m_Latarka0.activeSelf)
        {
            for(int i = 0; i < m_LatarkaPoints.Length; i++)
                m_LatarkaPoints[i] = Vector3.positiveInfinity;
            
            return;
        }

        float camLocalX = Vector3.Angle(transform.forward, m_LatarkaTransform.forward);

        if(m_LatarkaTransform.forward.y < 0.0f)
            camLocalX = -camLocalX;

        float subSteps = (float)latarkaSubSteps;

        int pt = 0;
        foreach(Vector3 dir in m_LatarkaPointDirs)
        {
            Vector3 dirRotated = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * dir;
            dirRotated = Quaternion.AngleAxis(camLocalX, -m_LatarkaTransform.right) * dirRotated;

            for(float i = 1.0f; i <= subSteps && pt < m_LatarkaPoints.Length; i+=1.0f)
            {
                Vector3 targetDir = Vector3.Slerp(m_LatarkaTransform.forward, dirRotated, i/subSteps);

                RaycastHit hit;
                if (Physics.Raycast(m_LatarkaTransform.position, targetDir, out hit, m_LatarkaRange, m_LatarkaLayer))
                    m_LatarkaPoints[pt] = hit.point;
                else
                    m_LatarkaPoints[pt] = Vector3.positiveInfinity;

                pt++;
            }
        }
    }

    void Update()
    {
        if(DevConsole.m_IsOpen)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_Latarka0.SetActive(!m_Latarka0.activeSelf);
            m_Latarka1.SetActive(!m_Latarka1.activeSelf);
        }
    }

    void OnDrawGizmos()
    {
        if(m_LatarkaPoints != null)
        foreach(Vector3 p in m_LatarkaPoints)
        {
            Gizmos.color = Color.yellow * new Color(1f,1f,1f, 0.2f);
            Gizmos.DrawLine(transform.position, p);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(p, 0.1f);
        }
    }

}