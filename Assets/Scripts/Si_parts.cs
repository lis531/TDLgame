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
        //if Enemy is not near to the Player then teleport Enemy near to the Player
        if (Enemy.transform.position.x - Player.transform.position.x > 5)
        {
            Enemy.transform.position = new Vector3(Player.transform.position.x + 5, Player.transform.position.y, Player.transform.position.z);
        }
    }
    //Enemy can't see player through the wall
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Enemy.transform.position = new Vector3(Player.transform.position.x + 5, Player.transform.position.y, Player.transform.position.z);
        }
    }
    //Enemy can't go throught closed door
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

}