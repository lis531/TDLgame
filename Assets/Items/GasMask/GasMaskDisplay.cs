using UnityEngine;

public class GasMaskDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasGasMask ? 1f : 0f, 1f, 1f);
    }
}