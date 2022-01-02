using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class inteligencja : MonoBehaviour
{
    public GameObject Target;
    public float speed = 1.5f;
    public Transform other;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.gameObject.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            Debug.log("Distance to other" + dist);
        }       
    }
}