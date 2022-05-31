using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public static int health = Health.health;
    public static float stamina = PlayerStamina.stamina;
    public void SavePlayer()
    {
        SaveData.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayer();
        if (data == null)
        {
            Debug.Log("Save file not found");
            return;
        }
        else
        {
            SceneManager.LoadScene("Podziemie");
            health = data.health;
            stamina = data.stamina;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            PlayerInventory.hasKeycard = data.inventory[0];
            PlayerInventory.hasMedkit = data.inventory[1];
            PlayerInventory.hasGoggles = data.inventory[2];
        }
    }
}