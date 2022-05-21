using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene("Podziemie");
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}