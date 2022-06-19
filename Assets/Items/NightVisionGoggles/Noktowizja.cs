using UnityEngine;
using UnityEngine.Rendering;

public class Noktowizja : MonoBehaviour
{
    public GameObject enemycialo;
    public GameObject enemy;
    public GameObject m_localVolume;
    public Volume volume;
    public VolumeProfile NightVisonProfile;
    public VolumeProfile GlobalVolumeProfile;
    public static bool m_TurnedOn = false;
    public static float m_Battery = 100;
    public static float m_BatteryUsage = 2;

    void Start()
    {
        m_localVolume.SetActive(false);
        enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
        enemy.GetComponent<AudioSource>().volume = 0;
    }   

    public void SwitchState(bool onOff)
    {
        if (onOff)
        {
            m_TurnedOn = true;
            m_localVolume.SetActive(true);
            volume.profile = NightVisonProfile;
            enemy.GetComponent<AudioSource>().volume = 1;
            enemycialo.transform.localScale = new Vector3(70f, 70f, 60f);
        }
        else
        {
            m_TurnedOn = false;
            m_localVolume.SetActive(false);
            volume.profile = GlobalVolumeProfile;
            enemy.GetComponent<AudioSource>().volume = 0;
            enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    void Update()
    {
        if (PlayerInventory.hasGoggles && !Pause.escOpened && !Inventory.on && Input.GetKeyDown("n") && m_Battery > 0 )
        {
            if (!m_TurnedOn)
            {
                m_TurnedOn = true;
                m_localVolume.SetActive(true);
                volume.profile = NightVisonProfile;
                enemy.GetComponent<AudioSource>().volume = 1;
                enemycialo.transform.localScale = new Vector3(70f, 70f, 60f);
            }
            else if (m_TurnedOn)
            {
                m_TurnedOn = false;
                m_localVolume.SetActive(false);
                volume.profile = GlobalVolumeProfile;
                enemy.GetComponent<AudioSource>().volume = 0;
                enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
        if (m_TurnedOn)
        {
            m_Battery -= m_BatteryUsage * Time.deltaTime;
            if (m_Battery <= 0)
            {
                m_Battery = 0;
                SwitchState(false);
            }
        }
    }
}