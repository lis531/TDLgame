using UnityEngine;

public class Batteries : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && PlayerInventory.hasBatteries && !Noktowizja.m_TurnedOn && Latarka.m_Battery != 100 && Time.timeScale != 0)
        {
            PlayerInventory.batteryCount -= 1;
            Latarka.m_Battery = 100;
        }
        else if(Input.GetKeyDown(KeyCode.R) && PlayerInventory.hasBatteries && Noktowizja.m_TurnedOn && Noktowizja.m_Battery != 100 && Time.timeScale != 0)
        {
            PlayerInventory.batteryCount -= 1;
            Noktowizja.m_Battery = 100;
        }
    }
}