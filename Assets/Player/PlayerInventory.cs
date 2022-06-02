using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    static public bool hasKeycard = false;
    static public bool hasMedkit;
    static public bool hasGoggles = false;
    static public bool hasBezpiecznik;
    static public int medkitCount = 0;
    static public int bezpiecznikCount = 0;
    void Start()
    {
        bezpiecznikCount = Random.Range(1, 3);
    }
    public static void MedkitCount()
    {
        if (medkitCount > 0)
        {
            hasMedkit = true;
            Debug.Log(medkitCount);
        }
        else
        {
            hasMedkit = false;
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
        if (bezpiecznikCount == 4)
        {
            DoorController.m_Locked = false;
            Debug.Log("Otwieranie drzwi");
        }
    }
}