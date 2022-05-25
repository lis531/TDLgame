using UnityEngine;
using System.Collections;

public class TunnelMoving : MonoBehaviour
{
    // MEMBERZY TunnelMoving.cs //
    const float sensitivity = 200.0f;

    public float walkSpeed = 4.0f;
    public float crouchSpeed = 1.2f;
    public float runSpeed  = 6.0f;
    float currentSpeed = 3.5f;

    private Animation anim;
    private AudioSource aSource;
    private CharacterController character;
    private GameObject cam;

    float targetCamRot = 0;

    public AudioClip[] stepSounds;
    public float walkStepOffset;
    public float crouchStepOffset;
    public float runStepOffset;
    float currentStepOffset;

    public float crouchHeightOffset;

    bool isStepping = false;
    bool isRunning = false;
    bool isCrouching = false;

    // METODY TunnelMoving.cs//
    bool PlayerMoves()
    {
        if ((Input.GetAxis("Vertical") != 0) || ( Input.GetAxis("Horizontal") != 0))
            return true;
        else return false;
    }

    void BeginRun()
    {
        if (!isCrouching)
        {
            currentSpeed = runSpeed;
            currentStepOffset = runStepOffset;
            isRunning = true;
        }
    }
    void EndRun()
    {
        if (!isCrouching)
        {
            currentSpeed = walkSpeed;
            currentStepOffset = walkStepOffset;
            isRunning = false;

            PlayerStamina.instance.TryMakePlayerTired();
        }
    }

    void BeginCrouch()
    {
        isCrouching = true;
        currentSpeed = crouchSpeed;
        currentStepOffset = crouchStepOffset;

        anim.clip = anim.GetClip("Crouch");
        anim.Play();
    }
    void EndCrouch()
    {
        isCrouching = false;
        currentSpeed = walkSpeed;
        currentStepOffset = walkStepOffset;

        anim.clip = anim.GetClip("UnCrouch");
        anim.Play();
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
        character = GetComponent<CharacterController>();
        aSource  = GetComponent<AudioSource>();
        anim    = GetComponent<Animation>();
        cam    = transform.GetChild(0).gameObject;

        currentSpeed = walkSpeed;
        currentStepOffset = walkStepOffset;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Bieganie
            // Rozpoczynanie biegania
            if (Input.GetKey(KeyCode.LeftShift) && !PlayerStamina.instance.exhausted)
                BeginRun();

            // Konczenie biegania
            else if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerStamina.instance.exhausted)
                EndRun();

        // Stamina
            // Zuzycie / Regeneracja staminy
            if (isRunning)
                PlayerStamina.instance.TryConsumeStamina();
            else
                PlayerStamina.instance.TryRegenStamina();

        // Kucanie
            if (Input.GetKeyDown(KeyCode.LeftControl))
                BeginCrouch();
            else if (Input.GetKeyUp(KeyCode.LeftControl))
                EndCrouch();


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