using UnityEngine;
using UnityEngine.Rendering;

public class EnemyNoktowizja : MonoBehaviour
{
    public GameObject enemycialo;
    public GameObject enemy;
    public GameObject volume;
    void Update()
    {
        if (!NightVisionGoggles.TurnedOn)
        {
            volume.SetActive(false);
            enemy.GetComponent<AudioSource>().volume = 0;
            enemycialo.transform.localScale = new Vector3(0f, 0f, 0f);

        }
        else
        {
            volume.SetActive(true);
            enemy.GetComponent<AudioSource>().volume = 1;
            enemycialo.transform.localScale = new Vector3(70f, 70f, 60f);
        }
    }
}
