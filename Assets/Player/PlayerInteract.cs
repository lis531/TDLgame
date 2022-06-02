using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance = 3.0f;

    public string keycardTag = "Keycard";
    public string doorTag = "DoorPart";
    public string elevatorTag = "ElevatorPart";
    public string medkitTag = "Medkit";
    public string gogglesTag = "Goggles";
    public string bezpiecznikTag = "Bezpiecznik";
    public string bezpiecznikboxTag = "BezpiecznikBox";

    void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag(keycardTag) && !PlayerInventory.hasKeycard)
            {
                PlayerInventory.hasKeycard = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(medkitTag) && !PlayerInventory.hasMedkit)
            {
                PlayerInventory.hasMedkit = true;
                Destroy(hit.collider.gameObject);
                PlayerInventory.medkitCount =+ 1;
            }
            else if (hit.collider.CompareTag(gogglesTag))
            {
                PlayerInventory.hasGoggles = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(bezpiecznikTag))
            {
                PlayerInventory.hasBezpiecznik = true;
                Destroy(hit.collider.gameObject);
                PlayerInventory.bezpiecznikCount =+ 1;
            }
            else if (hit.collider.CompareTag(bezpiecznikboxTag) && PlayerInventory.hasBezpiecznik)
            {
                PlayerInventory.hasBezpiecznik = false;
                BezpiecznikBox.Add();
            }
            else if (hit.collider.CompareTag(doorTag))
            {
                hit.collider.transform.parent.gameObject.GetComponent<DoorController>().Door();
            }
            else if (hit.collider.CompareTag(elevatorTag))
            {
                hit.collider.transform.GetComponent<ButtonPanelController>().PressButton();
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            Interact();
        }
    }
}
