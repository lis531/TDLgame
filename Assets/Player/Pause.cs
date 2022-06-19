using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject Esc;
    public static GameObject enemy;
    public static bool Paus;

    void Start()
    {
        Esc.transform.localScale = new Vector3(0, 0, 0);
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UnPaused();
        }
    }
    public void Paused()
    {
        Paus = true;
        Time.timeScale = 0;
        enemy.GetComponent<AudioSource>().volume = 0;
    }
    public void UnPaused()
    {
        Paus = false;
        Time.timeScale = 1;
        enemy.GetComponent<AudioSource>().volume = 1;
    }
    public void Play()
    {
        escOpened = false;
        Esc.transform.localScale = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        enemy.GetComponent<AudioSource>().volume = 1;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
