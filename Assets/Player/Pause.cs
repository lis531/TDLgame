using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static GameObject Esc;
    public static GameObject enemy;
    public static bool inWork;
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        Esc = GameObject.Find("Esc");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inWork && !Inventory.invOn)
        {
            inWork = true;
            Esc.transform.localScale = new Vector3(1, 1, 1);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            enemy.GetComponent<AudioSource>().volume = 0;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inWork && !Inventory.invOn)
        {
            inWork = false;
            UnPaused();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !inWork && Inventory.invOn)
        {
            Inventory.invOn = false;
            UnPaused();
        }
    }
    void UnPaused()
    {
        Esc.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 1;
        enemy.GetComponent<AudioSource>().volume = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Play()
    {
        UnPaused();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        UnPaused();
    }
}
