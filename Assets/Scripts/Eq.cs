using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq : MonoBehaviour
{
    public GameObject EquimpentUI;
    void Start()
    {
       
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {           
          
            if(transform.localScale.x == 1)
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
       
    }
}
