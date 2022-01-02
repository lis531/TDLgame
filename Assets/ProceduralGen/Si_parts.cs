using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_parts : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //if object is not near to the player object then teleport it near to the player object
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 10)
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
