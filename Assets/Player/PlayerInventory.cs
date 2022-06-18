using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    static public bool hasKeycard;
    static public bool hasMedkit;
    static public bool hasGoggles;
    static public bool hasBezpiecznik;
    static public bool hasGasMask;
    static public int medkitCount = 0;
    static public int bezpiecznikCount = 0;
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
        if (bezpiecznikIn == 4)
        {
            DoorController.m_Locked = false;
            Debug.Log("Otwieranie drzwi");
        }
    }
}