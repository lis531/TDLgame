using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animation elevatorAnim;
    Animation cameraAnim;
    Animation menuAnim;

    IEnumerator FlyAndLoad()
    {
        elevatorAnim = GameObject.Find("Elevator").GetComponent<Animation>();
        cameraAnim = GameObject.Find("Main Camera").GetComponent<Animation>();
        menuAnim = transform.GetComponent<Animation>();

        cameraAnim.Play();
        elevatorAnim.Play();
        menuAnim.Play();

        yield return new WaitForSeconds(cameraAnim.clip.length);
        SceneManager.LoadScene("Podziemie");
    }
    public void PlayGame ()
    {
        StartCoroutine(FlyAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}