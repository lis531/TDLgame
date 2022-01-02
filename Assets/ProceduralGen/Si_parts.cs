using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream:Assets/ProceduralGen/Si_parts.cs
using UnityEngine.GameObject;
=======
using UnityEngine;
>>>>>>> Stashed changes:Assets/Scripts/Si_parts.cs

public class Si_parts : MonoBehaviour
{
    GameObject Player;
    GameObject Wall;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find(Wall);
        //if object is not near to the player object then teleport it near to the player object
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 10)
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
    }
    //can't see player through the wall
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Player = other.gameObject;
            Player.GetComponent<Player_move>().can_see = false;
        }
    }
    //can't go throught closed door
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Player = other.gameObject;
            Player.GetComponent<Player_move>().can_see = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}