using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float stamina;
    public float[] position;
    public bool[] inventory;

    public PlayerData(Save save)
    {
        stamina = save.stamina;
        health = save.health;
        position = new float[3];
        position[0] = save.transform.position.x;
        position[1] = save.transform.position.y;
        position[2] = save.transform.position.z;
        inventory = new bool[3];
        inventory[0] = PlayerInventory.hasKeycard;
        inventory[1] = PlayerInventory.hasMedkit;
        inventory[2] = PlayerInventory.hasGoggles;
    }
}
