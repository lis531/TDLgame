using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Noktowizja : MonoBehaviour
{
    public GameObject enemycialo;
    public GameObject enemy;
    public GameObject m_localVolume;
    public Volume volume;
    public VolumeProfile NightVisonGlobalVolume;
    public VolumeProfile MainGlobalVolume;
    public static bool m_TurnedOn;
    public static bool canBe;
    public static float m_Battery = 100;
    public static float m_BatteryUsage = 2;

    void Start()
    {
        m_localVolume.SetActive(false);
        if (m_TurnedOn)
        {
            SwitchState(true);
        }
        else if (!m_TurnedOn)
        {
            SwitchState(false);
        }
    }   

    public void SwitchState(bool onOff)
    {
        if (onOff)
        {
            //change enviroment lighting ambient color
            RenderSettings.ambientLight = new Color32(0, 45, 0, 0);

            m_TurnedOn = true;
            canBe = true;
            m_localVolume.SetActive(true);
            volume.profile = NightVisonGlobalVolume;
            enemy.GetComponent<AudioSource>().volume = 1;
            enemycialo.transform.localScale = new Vector3(70f, 70f, 60f);
        }
        else
        {
            RenderSettings.ambientLight = new Color32(0, 0, 0, 0);
            m_TurnedOn = false;
            m_localVolume.SetActive(false);
            volume.profile = MainGlobalVolume;
            enemy.GetComponent<AudioSource>().volume = 0;
            enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    void Update()
    {
        if (PlayerInventory.hasGoggles && Time.timeScale != 0 && Input.GetKeyDown("n") && m_Battery > 0 )
        {
            if (!m_TurnedOn)
            {
                SwitchState(true);
            }
            else if (m_TurnedOn)
            {
                SwitchState(false);
                canBe = false;
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