using UnityEngine;

public class BatteriesDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasBatteries ? 1f : 0f, 1f, 1f);
    }
}
