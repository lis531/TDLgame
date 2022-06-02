using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    static public bool hasKeycard = false;
    static public bool hasMedkit;
    static public bool hasGoggles = false;
    static public bool hasBezpiecznik;
    static public int medkitCount = 0;
    static public int bezpiecznikCount = 0;
    void Update()
    {
        if (medkitCount => 0)
        {
            hasMedkit = true;
        }
        else
        {
            hasMedkit = false;
        }
        if (bezpiecznikCount => 0)
        {
            hasBezpiecznik = true;
        }
        else
        {
            hasBezpiecznik = false;
        }
    }
}