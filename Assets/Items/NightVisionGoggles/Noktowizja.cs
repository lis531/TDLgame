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

    void Start()
    {
        m_localVolume.SetActive(false);
        enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
        enemy.GetComponent<AudioSource>().volume = 0;
    }    
    void Update()
    {
        if (PlayerInventory.hasGoggles && !Pause.escOpened)
        {
            if (Input.GetKeyDown("n") && !m_TurnedOn)
            {
                m_TurnedOn = true;
                m_localVolume.SetActive(true);
                volume.profile = NightVisonProfile;
                enemy.GetComponent<AudioSource>().volume = 1;
                enemycialo.transform.localScale = new Vector3(70f, 70f, 60f);
            }
            else if (Input.GetKeyDown("n") && m_TurnedOn && !Pause.escOpened)
            {
                m_TurnedOn = false;
                m_localVolume.SetActive(false);
                volume.profile = GlobalVolumeProfile;
                enemy.GetComponent<AudioSource>().volume = 0;
                enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }
}