using UnityEngine;

public class NightVisionGoggles : MonoBehaviour
{
    public GameObject NightVision;
    public GameObject GlobalVolume;
    bool TurnedOn;
    void Update()
    {
        if (Input.GetKeyDown("n") && PlayerInventory.hasGoggles && !TurnedOn)
        {
            NightVision.SetActive(NightVision.activeInHierarchy);
            GlobalVolume.SetActive(!GlobalVolume.activeInHierarchy);
            TurnedOn = true;
        }
        else if (Input.GetKeyDown("n") && TurnedOn)
        {
            NightVision.SetActive(!NightVision.activeInHierarchy);
            GlobalVolume.SetActive(GlobalVolume.activeInHierarchy);
            TurnedOn = false;
        }

    }
}