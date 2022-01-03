using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    //create health variable
    public int health = 100;
    //create variable for the gameobject
    public GameObject Player;
    public GameObject Enemy;
    //if enemy touching the Player then Player loses 10 health every second
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            health -= 10;
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}