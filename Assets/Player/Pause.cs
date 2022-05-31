using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject Esc;

    void Start()
    {
        Esc.transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Esc.transform.localScale = new Vector3(1, 1, 1);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
    public void Play()
    {
        Esc.transform.localScale = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
