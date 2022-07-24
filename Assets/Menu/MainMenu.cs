using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject elevator;
    public GameObject mCamera;
    public GameObject fadeOut;
    public GameObject options;
    public GameObject menuLight;
    bool Animation = false;

    Animation elevatorAnim;
    Animation cameraAnim;
    Animation menuAnim;
    Animation fadeOutAnim;
    Animation optionsAnim;
    Animation lightAnim;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        lightAnim = menuLight.GetComponent<Animation>();
        elevatorAnim = elevator.GetComponent<Animation>();
        cameraAnim = mCamera.GetComponent<Animation>();
        menuAnim = transform.GetComponent<Animation>();
        optionsAnim = options.GetComponent<Animation>();
        fadeOutAnim = fadeOut.GetComponent<Animation>();
    }

    IEnumerator FlyAndLoad()
    {
        cameraAnim.clip = cameraAnim.GetClip("FlyToElevator");
        menuAnim.clip = menuAnim.GetClip("DisappearMenu");
        
        cameraAnim.Play();
        elevatorAnim.Play();
        menuAnim.Play();
        StartCoroutine(WaitForFade());

        yield return new WaitForSeconds(cameraAnim.clip.length);
        SceneManager.LoadScene("Tunele");
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        Animation = false;
    }
    IEnumerator WaitForFading()
    {
        yield return new WaitForSeconds(1.4f);
        Application.Quit();
    }
    IEnumerator WaitForFade()
    {
        lightAnim.clip = lightAnim.GetClip("lightFade");
        lightAnim.Play();
        yield return new WaitForSeconds(1.2f);
        fadeOutAnim.Play();
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
            lightAnim.clip = lightAnim.GetClip("lightAtOptions");
            lightAnim.Play();
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
            lightAnim.clip = lightAnim.GetClip("lightAtMenu");
            lightAnim.Play();
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
        fadeOutAnim.Play();
        StartCoroutine(WaitForFading());
    }
}