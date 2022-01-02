using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    //create health variable
    public int health = 100;
    //create variable for the gameobject
    public GameObject Player;
    public GameObject Enemy;
    //if enemy touch the Player then Player lose 10 health
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            health -= 10;
        }
    }
    //if hp is 0 then restart the game
    void Update()
    {
        if (health <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
