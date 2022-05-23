using UnityEngine;

public class Medkit : MonoBehaviour
{
    void Update()
    {
        if (PlayerInventory.hasMedkit && Input.GetKeyDown("h"))
        {
            Health.health += 100;
            PlayerInventory.hasMedkit = false;
            Destroy(gameObject);
        }
    }
}