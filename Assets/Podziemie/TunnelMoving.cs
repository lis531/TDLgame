using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMoving : MonoBehaviour
{
    const float sensitivity = 300.0f;
    const float speed = 5.0f;

    CharacterController character;
    private GameObject camera;

    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        camera = transform.GetChild(0).gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        character.Move(
            ((Input.GetAxis("Vertical") * transform.forward) + // Przod / Tyl
            (Input.GetAxis("Horizontal") * transform.right)) * // Lewo / Prawo
            speed * Time.deltaTime                           // Skalowanie
        );

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0));
        camera.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -sensitivity * Time.deltaTime, 0, 0));
    }
}
