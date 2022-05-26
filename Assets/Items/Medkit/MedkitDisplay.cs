using UnityEngine;

public class MedkitDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasMedkit ? 1f : 0f, 1f, 1f);
    }
}
