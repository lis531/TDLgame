using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 0.5f;
    public Vector3 deltaMove;
    public float speed = 1;
    private float speed1 = 1.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    turn.x += Input.GetAxis("Mouse X") * sensitivity;
    turn.y += Input.GetAxis("Mouse Y") * sensitivity;
    transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    transform.Translate(Input.GetAxisRaw("Horizontal") * speed1, 0, Input.GetAxisRaw("Vertical") * speed1);
    }
}