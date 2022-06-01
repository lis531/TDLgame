using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float stamina;
    public float[] position;
    public bool[] inventory;

    public PlayerData(Save player)
    {
        health = player.health;
        stamina = player.stamina;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        inventory = new bool[3];
        inventory[0] = PlayerInventory.hasKeycard;
        inventory[1] = PlayerInventory.hasMedkit;
        inventory[2] = PlayerInventory.hasGoggles;
    }
}
