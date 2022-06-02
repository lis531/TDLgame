using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{   
    public float health = Health.health;
    public float stamina = PlayerStamina.stamina;
    private GameObject player;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        SceneManager.LoadScene("Podziemie");
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            if (SceneManager.GetActiveScene().name == "Podziemie")
            {
                health = data.health;
                stamina = data.stamina;
                transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                PlayerInventory.hasKeycard = data.inventory[0];
                PlayerInventory.hasMedkit = data.inventory[1];
                PlayerInventory.hasGoggles = data.inventory[2];
            }
        }
        else
        {
            Debug.Log("No save file found");
        }
    }
}