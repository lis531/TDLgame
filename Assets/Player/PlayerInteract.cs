using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance;

    public string keycardTag = "Keycard";
    public string doorTag = "DoorPart";
    public string elevatorTag = "ElevatorPart";
    public string medkitTag = "Medkit";
    public string gogglesTag = "Goggles";
    public string bezpiecznikTag = "Bezpiecznik";
    public string bezpiecznikboxTag = "BezpiecznikBox";
    public string gasMaskTag = "GasMask";
    public string batteryTag = "Battery";

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
            else if (hit.collider.CompareTag(medkitTag))
            {
                Destroy(hit.collider.gameObject);
                PlayerInventory.medkitCount += 1;
                PlayerInventory.MedkitCount();
            }
            else if (hit.collider.CompareTag(gogglesTag))
            {
                PlayerInventory.hasGoggles = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(gasMaskTag))
            {
                PlayerInventory.hasGasMask = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(batteryTag))
            {
                Destroy(hit.collider.gameObject);
                PlayerInventory.batteryCount += 1;
                PlayerInventory.batteryCounting();
            }
            else if (hit.collider.CompareTag(bezpiecznikTag))
            {
                PlayerInventory.hasBezpiecznik = true;
                Destroy(hit.collider.gameObject);
                PlayerInventory.bezpiecznikCount =+ 1;
                PlayerInventory.BezpiecznikCount();
            }
            else if (hit.collider.CompareTag(bezpiecznikboxTag) && PlayerInventory.hasBezpiecznik)
            {
                PlayerInventory.hasBezpiecznik = false;
                PlayerInventory.bezpiecznikCount -= 1;
                PlayerInventory.bezpiecznikIn += 1;
                PlayerInventory.BezpiecznikCount();
            }
            else if (hit.collider.CompareTag(doorTag))
            {
                hit.collider.transform.parent.gameObject.GetComponent<DoorController>().OpenDoor();
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
