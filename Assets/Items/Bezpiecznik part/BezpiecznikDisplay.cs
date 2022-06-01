using UnityEngine;

public class BezpiecznikDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasBezpiecznik ? 1f : 0f, 1f, 1f);
    }
}
