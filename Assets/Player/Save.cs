using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{   
    public float health = Health.health;
    public float stamina = PlayerStamina.stamina;
    public bool hasKeycard = PlayerInventory.hasKeycard;
    public bool hasGoggles = PlayerInventory.hasGoggles;
    public int medkitCount = PlayerInventory.medkitCount;
    public GameObject player;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            SceneManager.LoadScene("Podziemie");
            Debug.Log("file exists");
            if (SceneManager.GetActiveScene().name == "Podziemie")
            {
                Debug.Log("Scene is Podziemie");
                player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                health = data.health;
                stamina = data.stamina;
                hasKeycard = data.inventory[0];
                hasGoggles = data.inventory[1];
                medkitCount = data.inventoryCountable;
            }
        }
        else
        {
            Debug.Log("No save file found");
        }
    }
}