using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public int health = 21;
    public float stamina = 33;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        Debug.Log("Loading Player2");
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            Debug.Log("Save file not found");
            return;
        }
        else
        {
            health = data.health;
            stamina = data.stamina;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            PlayerInventory.hasKeycard = data.inventory[0];
            PlayerInventory.hasMedkit = data.inventory[1];
            PlayerInventory.hasGoggles = data.inventory[2];
            SceneManager.LoadScene("Podziemie");
        }
    }
}