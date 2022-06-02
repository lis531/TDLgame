using UnityEngine;
using UnityEngine.Rendering;

public class Noktowizja : MonoBehaviour
{
    public GameObject enemycialo;
    public GameObject enemy;
    public GameObject localVolume;
    public Volume volume;
    public VolumeProfile m_NightVisonProfile;
    public VolumeProfile m_GlobalVolumeProfile;
    public static bool m_TurnedOn = false;
    
    void Update()
    {
        if (PlayerInventory.hasGoggles)
        {
            if (Input.GetKeyDown("n") && !m_TurnedOn)
            {
                m_TurnedOn = true;
                localVolume.SetActive(true);
                volume.profile = m_NightVisonProfile;
                enemy.GetComponent<AudioSource>().volume = 1;
                enemycialo.transform.localScale = new Vector3(70f, 70f, 60f);
            }
            else if (Input.GetKeyDown("n") && m_TurnedOn)
            {
                m_TurnedOn = false;
                localVolume.SetActive(false);
                volume.profile = m_GlobalVolumeProfile;
                enemy.GetComponent<AudioSource>().volume = 0;
                enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }
}