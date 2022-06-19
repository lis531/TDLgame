using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    static public bool hasKeycard;
    static public bool hasMedkit;
    static public bool hasGoggles;
    static public bool hasBezpiecznik;
    static public bool hasGasMask;
    static public bool hasBatteries;
    static public int batteryCount;
    static public int medkitCount;
    static public int bezpiecznikCount;
    static public int bezpiecznikIn;
    void Start()
    {
        bezpiecznikIn = Random.Range(1, 3);
    }
    public static void MedkitCount()
    {
        if (medkitCount > 0)
        {
            hasMedkit = true;
        }
        else
        {
            hasMedkit = false;
        }
    }
    public static void batteryCounting()
    {
        if (batteryCount > 0)
        {
            hasBatteries = true;
        }
        else
        {
            hasBatteries = false;
        }
    }
    public static void BezpiecznikCount()
    {
        if (bezpiecznikCount > 0)
        {
            hasBezpiecznik = true;
        }
        else
        {
            hasBezpiecznik = false;
        }
        if (bezpiecznikIn == 4)
        {
            DoorController.m_Locked = false;
            Debug.Log("Otwieranie drzwi");
        }
    }
}