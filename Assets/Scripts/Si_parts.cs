using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_parts : MonoBehaviour
{
    public GameObject Player;
    public GameObject Wall;
    public GameObject Enemy;
    public GameObject Door;
    void Start()
    {
        //if Enemy isn's near to the Player then teleport Enemy near to the Player
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 100)
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
        }
    }
    //Enemy can't see throught door 
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            Enemy.transform.position = new Vector3(Player.transform.position.x + 5, Player.transform.position.y, Player.transform.position.z);
        }
    }
    //slowly move Enemy to the Player
    void Update()
    {
        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, 0.1f);
    }
    //Enemy can't see player and go through the wall
}