using UnityEngine;
using System.Collections;

public class TunnelMoving : MonoBehaviour
{
    // MEMBERZY TunnelMoving.cs //
    const float sensitivity = 200.0f;

    public float walkSpeed = 4.0f;
    public float runSpeed  = 6.0f;
    float currentSpeed = 3.5f;

    private StaminaBar staminaBar;
    private CharacterController character;
    private GameObject cam;

    float targetCamRot = 0;

    AudioSource aSource;
    public AudioClip[] stepSounds;
    public float walkStepOffset;
    public float runStepOffset;
    float currentStepOffset;

    bool isStepping = false;
    bool isRunning = false;

    // METODY TunnelMoving.cs//
    bool PlayerMoves()
    {
        if ((Input.GetAxis("Vertical") != 0) || ( Input.GetAxis("Horizontal") != 0))
            return true;
        else return false;
    }

    void BeginRun()
    {
        currentSpeed = runSpeed;
        currentStepOffset = runStepOffset;
        isRunning = true;
    }
    void EndRun()
    {
        currentSpeed = walkSpeed;
        currentStepOffset = walkStepOffset;
        isRunning = false;
    }


    IEnumerator PlayStep()
    {
        isStepping = true;

        if (stepSounds.Length != 0)
        {
            AudioClip clip = stepSounds[Random.Range(0, stepSounds.Length)];
            aSource.PlayOneShot(clip);
        }
        else
            Debug.LogError("Dzwieki krokow nie dzialaja, bo nie ma zadnych dzwiekow przypisanych do skryptu \"TunnelMoving\" na Playerze");


        aSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.volume = Random.Range(0.75f, 1.0f);

        yield return new WaitForSeconds(currentStepOffset);

        if (PlayerMoves())
            StartCoroutine(PlayStep());
        else
            isStepping = false;
    }
    
    // FUNCKCJE UNITY //
    void Start()
    {
        staminaBar = GameObject.Find("Canvas/Stamina").GetComponent<StaminaBar>();
        character = gameObject.GetComponent<CharacterController>();
        aSource = gameObject.GetComponent<AudioSource>();
        cam = transform.GetChild(0).gameObject;

        currentSpeed = walkSpeed;
        currentStepOffset = walkStepOffset;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rozpoczynanie biegania
        if (Input.GetKey(KeyCode.LeftShift) && staminaBar.canRun)
            BeginRun();

        // Konczenie biegania
        else if (Input.GetKeyUp(KeyCode.LeftShift) || !staminaBar.canRun)
            EndRun();

        // Zuzycie / Regeneracja staminy
        if (isRunning)
            staminaBar.UseStamina();
        else
            staminaBar.RegenStamina();


        // Poruszanie sie postaci
        character.Move(
        ((Input.GetAxis("Vertical") * transform.forward) + // Przod / Tyl
        (Input.GetAxis("Horizontal") * transform.right)) * // Lewo / Prawo
        currentSpeed * Time.deltaTime);                    // Skalowanie
        
        if (PlayerMoves() && !isStepping)
            StartCoroutine(PlayStep());

        // Obrot postaci lewo/prawo
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0));

        // Ruch kamera gora/dol
        targetCamRot += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        targetCamRot = Mathf.Clamp(targetCamRot, -89.99f, 89.99f);

        cam.transform.localRotation = Quaternion.Euler(-targetCamRot, 0, 0);
    }
}