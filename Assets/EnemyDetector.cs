using UnityEngine;

public class EnemyDetector : MonoBehaviour
{

    public Material m_ScreenMat;

    public float m_ScanTimeOffset = 2.0f;
    public float m_MaxDist = 10.0f;

    private AudioSource m_AudioSource;
    private Transform m_Enemy;
    private MeshRenderer m_MeshRenderer;
    private MeshRenderer m_ScreenMeshRenderer;
    private MeshRenderer m_HandleMeshRenderer;
    private Transform m_Camera;

    private bool m_IsScannerOn = false;

    void Start()
    {
        m_Camera = transform.parent.GetChild(0).transform;
        m_Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        m_AudioSource = GetComponent<AudioSource>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_ScreenMeshRenderer = transform.GetChild(1).GetComponent<MeshRenderer>();
        m_HandleMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();;

        m_ScreenMat.SetVector("_RelativeEnemyPos", new Vector2(100.0f, 100.0f));

        m_ScreenMat.SetInt("_Enabled", m_IsScannerOn ? 1 : 0);

        m_MeshRenderer.enabled = false;
        m_ScreenMeshRenderer.enabled = false;
        m_HandleMeshRenderer.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown("m"))
            SwitchState(!m_IsScannerOn);
    }

    public void SwitchState(bool onOff)
    {
        m_IsScannerOn = onOff;

        m_MeshRenderer.enabled = m_IsScannerOn;
        m_ScreenMeshRenderer.enabled = m_IsScannerOn;
        m_HandleMeshRenderer.enabled = m_IsScannerOn;

        m_ScreenMat.SetInt("_Enabled", m_IsScannerOn ? 1 : 0);

        if(m_IsScannerOn)
            InvokeRepeating("UpdateEnemyPos", m_ScanTimeOffset, m_ScanTimeOffset);
        else
            CancelInvoke("UpdateEnemyPos");
    }

    void UpdateEnemyPos()
    {
        Vector2 diff = new Vector2(m_Enemy.position.x, m_Enemy.position.z) - new Vector2(transform.position.x, transform.position.z);
        Vector2 dir = diff.normalized;

        float angle = -Vector2.SignedAngle(new Vector2(transform.forward.x, transform.forward.z), dir);
        float len = diff.magnitude / m_MaxDist;

        Vector2 relativeEnemyPos = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle) * len, Mathf.Cos(Mathf.Deg2Rad * angle) * len);

        m_ScreenMat.SetVector("_RelativeEnemyPos", relativeEnemyPos);

        m_AudioSource.Play();
    }
}
