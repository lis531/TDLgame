using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float stamina;
    public float[] position;
    public bool[] inventory;
    public int inventoryCountable;

    public PlayerData(Save player)
    {
        health = player.health;
        stamina = player.stamina;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        inventory = new bool[2];
        inventory[0] = player.hasKeycard;
        inventory[1] = player.hasGoggles;
        inventoryCountable = player.medkitCount;
    }
}
