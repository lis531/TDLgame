using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject Esc;
    public GameObject enemy;
    public static bool inWork;
    public GameObject player;
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
            if (Noktowizja.canBe)
            {
                player.GetComponent<Noktowizja>().SwitchState(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inWork)
        {
            inWork = false;
            UnPaused();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !inWork && Inventory.invOn)
        {
            Inventory.invOn = false;
            Inventory.inv.transform.localScale = new Vector3(0f, 0f, 0f);
            UnPaused();
        }
    }
    public void UnPaused()
    {
        Esc.transform.localScale = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        if (Noktowizja.canBe)
        {
            player.GetComponent<Noktowizja>().SwitchState(true);
        }
    }
    public void Play()
    {
        inWork = false;
        UnPaused();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Esc.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 1;
    }
}
