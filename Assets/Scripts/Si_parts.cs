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
        if (Vector3.Distance(Enemy.transform.position, Player.transform.position) > 5)
        {
            Enemy.transform.position = new Vector3(Player.transform.position.x + Random.Range(-5, 5), Player.transform.position.y + Random.Range(-5, 5), Player.transform.position.z + Random.Range(-5, 5));
        }
    }
    //Enemy can't see throught door 
    void OnTriggerStay(Collider Door)
    {
        if (Door.gameObject.tag == "Door")
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 100)
            {
                transform.position = GameObject.Find("Player").transform.position + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            }
        }
    }
    //slowly move Enemy to the Player
    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 100)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime * 0.5f);
        }
    }
    //Enemy can't see player and go through the wall
    void OnTriggerEnter(Collider Wall)
    {
        if (Wall.gameObject.tag == "Wall")
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 100)
            {
                transform.position = GameObject.Find("Player").transform.position + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            }
        }
    }
}