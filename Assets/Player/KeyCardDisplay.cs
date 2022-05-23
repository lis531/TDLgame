using UnityEngine;

public class KeyCardDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasKeycard ? 1f : 0f, 1f, 1f);
    }
}