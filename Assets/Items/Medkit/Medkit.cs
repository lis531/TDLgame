using UnityEngine;

public class Medkit : MonoBehaviour
{
    void Update()
    {
        if (PlayerInventory.hasMedkit && Input.GetKeyDown("h") && Health.health != 100 && Time.timeScale != 0)
        {
            Health.health = 100;
            PlayerInventory.medkitCount -= 1;
            PlayerInventory.MedkitCount();
        }
    }
}