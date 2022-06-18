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

    public float walkSmoothing = 5f;

    public float walkStepOffset, crouchStepOffset, runStepOffset, crouchHeightOffset;
    float currentStepOffset;

    private Vector2 velocity;

    private Animation anim;
    private AudioSource aSource;
    private CharacterController character;
    private GameObject cam;
    float targetCamRot = 0;
    public AudioClip[] stepSounds;

    bool isStepping = false;
    public static bool isRunning = false;
    public static bool isCrouching = false;

    // METODY TunnelMoving.cs//
    public static bool PlayerMoves()
    {
        if ((Input.GetAxisRaw("Vertical") != 0) || ( Input.GetAxisRaw("Horizontal") != 0))
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

            if(PlayerMoves())
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
    
    void ApplyGravity()
    {
        if (!character.isGrounded)
            character.Move(Vector3.down * Time.deltaTime * 9.81f);
    }

    // FUNCKCJE UNITY //
    void Start()
    {
        character = GetComponent<CharacterController>();
        aSource  = GetComponent<AudioSource>();
        anim    = GetComponent<Animation>();
        cam    = transform.GetChild(0).gameObject;

        velocity = new Vector2(0f,0f);

        currentSpeed = walkSpeed;
        currentStepOffset = walkStepOffset;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(DevConsole.m_IsOpen)
            return;

        ApplyGravity();

        // Bieganie
            // Rozpoczynanie biegania
            if (Input.GetKey(KeyCode.LeftShift) && !PlayerStamina.instance.exhausted && PlayerMoves())
                BeginRun();
            // Konczenie biegania
            else if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerStamina.instance.exhausted)
                EndRun();

        // Stamina
            // Zuzycie / Regeneracja staminy
            if (isRunning && PlayerMoves())
                PlayerStamina.instance.TryConsumeStamina();
            else
                PlayerStamina.instance.TryRegenStamina();

        // Kucanie
            if (Input.GetKeyDown(KeyCode.LeftControl))
                BeginCrouch();
            else if (Input.GetKeyUp(KeyCode.LeftControl))
                EndCrouch();

            Vector3 movement = (Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right).normalized;

            velocity = Vector2.Lerp(velocity, new Vector2(movement.x, movement.z), Time.deltaTime * walkSmoothing);

        // Poruszanie sie postaci
            character.Move(new Vector3(velocity.x, 0f, velocity.y) * currentSpeed * Time.deltaTime);
            
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