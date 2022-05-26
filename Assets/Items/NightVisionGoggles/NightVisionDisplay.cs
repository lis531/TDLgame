using UnityEngine;

public class NightVisionDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasGoggles ? 1f : 0f, 1f, 1f);
    }
}