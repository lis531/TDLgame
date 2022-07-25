using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance;

    public string keycard1Tag = "Keycard1";
    public string keycard2Tag = "Keycard2";
    public string keycard3Tag = "Keycard3";
    public string doorTag = "DoorPart";
    public string elevatorTag = "ElevatorPart";
    public string medkitTag = "Medkit";
    public string gogglesTag = "Goggles";
    public string bezpiecznikTag = "Bezpiecznik";
    public string bezpiecznikboxTag = "BezpiecznikBox";
    public string gasMaskTag = "GasMask";
    public string filterTag = "Filter";
    public string batteryTag = "Battery";

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag(keycard1Tag) && !PlayerInventory.hasKeycard1)
            {
                PlayerInventory.hasKeycard1 = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(keycard2Tag) && !PlayerInventory.hasKeycard2)
            {
                PlayerInventory.hasKeycard2 = true;
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(keycard3Tag) && !PlayerInventory.hasKeycard3)
            {
                PlayerInventory.hasKeycard3 = true;
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
            else if (hit.collider.CompareTag(filterTag))
            {
                Destroy(hit.collider.gameObject);
                PlayerInventory.filterCount += 1;
            }
            else if (hit.collider.CompareTag(batteryTag))
            {
                Destroy(hit.collider.gameObject);
                PlayerInventory.batteryCount += 1;
                PlayerInventory.batteryCounting();
            }
            else if (hit.collider.CompareTag(bezpiecznikTag))
            {
                Destroy(hit.collider.gameObject);
                PlayerInventory.bezpiecznikCount += 1;
                PlayerInventory.BezpiecznikCount();
            }
            else if (hit.collider.CompareTag(bezpiecznikboxTag) && PlayerInventory.hasBezpiecznik)
            {
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
        Debug.Log("medkitCount: " + PlayerInventory.medkitCount);
        Debug.Log("filterCount: " + PlayerInventory.filterCount);
        Debug.Log("batteryCount: " + PlayerInventory.batteryCount);
        Debug.Log("bezpiecznikCount: " + PlayerInventory.bezpiecznikCount);
        Debug.Log("bezpiecznikIn: " + PlayerInventory.bezpiecznikIn);
        if(Input.GetKeyDown("e") && Time.timeScale != 0)
        {
            Interact();
        }
    }
}
