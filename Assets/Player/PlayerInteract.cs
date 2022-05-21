using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance = 3.0f;

    public string keycardTag = "Keycard";
    public string doorTag = "DoorPart";

    void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag(keycardTag) && !PlayerInventory.hasKeycard)
            {
                PlayerInventory.hasKeycard = true;
                Destroy( hit.collider.gameObject);
            }

            else if (hit.collider.CompareTag(doorTag))
                hit.collider.transform.parent.gameObject.GetComponent<DoorController>().Open();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("e"))
            Interact();
    }
}
