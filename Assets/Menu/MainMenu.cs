using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject elevator;
    public GameObject mCamera;
    public GameObject fadeOut;
    public GameObject options;

    bool Animation = false;

    Animation elevatorAnim;
    Animation cameraAnim;
    Animation menuAnim;
    Animation fadeOutAnim;
    Animation optionsAnim;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        elevatorAnim = elevator.GetComponent<Animation>();
        cameraAnim = mCamera.GetComponent<Animation>();
        menuAnim = transform.GetComponent<Animation>();
        fadeOutAnim = fadeOut.GetComponent<Animation>();
        optionsAnim = options.GetComponent<Animation>();
    }

    IEnumerator FlyAndLoad()
    {
        cameraAnim.clip = cameraAnim.GetClip("FlyToElevator");
        menuAnim.clip = menuAnim.GetClip("DisappearMenu");

        cameraAnim.Play();
        elevatorAnim.Play();
        menuAnim.Play();
        fadeOutAnim.Play();

        yield return new WaitForSeconds(cameraAnim.clip.length);
        SceneManager.LoadScene("Tunele");
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        Animation = false;
    }

    public void PlayGame()
    {
        StartCoroutine(FlyAndLoad());
    }

    public void GoToOptions()
    {
        if (!Animation)
        {
            Animation = true;
            cameraAnim.clip = cameraAnim.GetClip("GoToOptions");
            cameraAnim.Play();

            menuAnim.clip = menuAnim.GetClip("DisappearMenu");
            menuAnim.Play();

            optionsAnim.clip = optionsAnim.GetClip("SettingsAppear");
            optionsAnim.Play();
            StartCoroutine(WaitForAnimation());
        }
    }
    public void GoBackToMenu()
    {
        if (!Animation)
        {
            Animation = true;
            cameraAnim.clip = cameraAnim.GetClip("GoToMenu");
            cameraAnim.Play();

            menuAnim.clip = menuAnim.GetClip("AppearMenu");
            menuAnim.Play();

            optionsAnim.clip = optionsAnim.GetClip("SettingsDisappear");
            optionsAnim.Play();
            StartCoroutine(WaitForAnimation());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}