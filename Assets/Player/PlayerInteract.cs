using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance = 3.0f;

    public string keycardTag = "Keycard";
    public string doorTag = "DoorPart";
    public string elevatorTag = "ElevatorPart";

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

                else if (hit.collider.CompareTag(elevatorTag))
                    hit.collider.transform.parent.gameObject.GetComponent<ElevatorController>().OpenElevator();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("e"))
            Interact();
    }
}
