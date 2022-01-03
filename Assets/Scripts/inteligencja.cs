using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class inteligencja : MonoBehaviour
{
    public GameObject Target;
    public float speed = 1.5f;
    public Transform other;
    public GameObject obiekt1;
    public GameObject obiekt2;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.gameObject.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        distance = Vector3.Distance(obiekt1.transform.position, obiekt2.transform.position);
        if (distance < 1)
        {            
            SceneManager.LoadScene(0);
          
        }
        
    }
}