using UnityEngine;
using System.Collections;

public class TunnelMoving : MonoBehaviour
{
    
    const float sensitivity = 200.0f;
    float speed = 3.5f;
    

    private CharacterController character;
    private GameObject cam;

    float targetCamRot = 0;

    AudioSource aSource;
    public AudioClip[] stepSounds;
    public float stepOffset = 0.5f;
    bool isStepping = false;

    bool PlayerMoves()
    {
        if (Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") != 0)
            return true;
        else return false;
    }

    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        aSource = gameObject.GetComponent<AudioSource>();
        cam = transform.GetChild(0).gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator PlayStep()
    {
        isStepping = true;

        AudioClip clip = stepSounds[Random.Range(0, stepSounds.Length)];
        aSource.PlayOneShot(clip);

        aSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.volume = Random.Range(0.75f, 1.0f);

        yield return new WaitForSeconds(stepOffset);

        if (PlayerMoves())
            StartCoroutine(PlayStep());
        else
            isStepping = false;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 6f;
            StaminaBar.instance.UseStamina(1);
            
        }
        else
        {
            speed = 3.5f;
        }

        
        
            // Poruszanie sie postaci
            character.Move(
            ((Input.GetAxis("Vertical") * transform.forward) + // Przod / Tyl
            (Input.GetAxis("Horizontal") * transform.right)) * // Lewo / Prawo
            speed * Time.deltaTime                           // Skalowanie
        );
        
      
        

        if (PlayerMoves() && !isStepping)
            StartCoroutine(PlayStep());

        // Obrot postaci lewo/prawo
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0));

        // Ruch kamera gora/dol
        targetCamRot += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        targetCamRot = Mathf.Clamp(targetCamRot, -89.99f, 89.99f);

        cam.transform.localRotation = Quaternion.Euler(-targetCamRot, 0, 0);
    }
    //player crouch
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Crouch")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (character.height == 1.8f)
                {
                    character.height = 1.5f;
                    character.center = new Vector3(0, 0.5f, 0);
                }
                else
                {
                    character.height = 1.8f;
                    character.center = new Vector3(0, 1.0f, 0);
                }
            }
        }
    }
}
