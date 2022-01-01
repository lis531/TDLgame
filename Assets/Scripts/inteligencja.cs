using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class inteligencja : MonoBehaviour
{
    public GameObject Target;
    public float speed = 1.5f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.gameObject.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
}
