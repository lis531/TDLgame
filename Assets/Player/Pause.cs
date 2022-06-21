using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static GameObject Esc;
    public static GameObject enemy;
    public static bool Paus;

    void Start()
    {
        enemy = GameObject.Find("Enemy");
        Esc = GameObject.Find("Esc");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Paus)
        {
            Esc.transform.localScale = new Vector3(1, 1, 1);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Paused();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Paus)
        {
            Esc.transform.localScale = new Vector3(0, 0, 0);
            Debug.Log("Trying");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UnPaused();
        }
        Debug.Log(Paus);
    }
    public static void Paused()
    {
        Paus = true;
        Time.timeScale = 0;
        enemy.GetComponent<AudioSource>().volume = 0;
    }
    public static void UnPaused()
    {
        Paus = false;
        Esc.transform.localScale = new Vector3(0, 0, 0);
        Inventory.inv.transform.localScale = new Vector3(0f, 0f, 0f);
        Time.timeScale = 1;
        enemy.GetComponent<AudioSource>().volume = 1;
    }
    public void Play()
    {
        Debug.Log("Play");
        UnPaused();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        UnPaused();
    }
}
