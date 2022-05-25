using UnityEngine;

public class NightVisionGoggles : MonoBehaviour
{
    bool TurnedOn;
    Exposure exposure;
    void Start()
    {
        GlobalVolume volume = gameObject.GetComponent<Exposure>();
        Exposure tmp;
        if (volume.profile.TryGet<Exposure>(out tmp))
        {
            exposure = tmp;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("n") && PlayerInventory.hasGoggles && !TurnedOn)
        {
            //change exposure settings to 8
            exposure.enabled.value = true;
            exposure.fixedExposure.value = 8;
            TurnedOn = true;
        }
        else if (Input.GetKeyDown("n") && TurnedOn)
        {
            TurnedOn = false;
        }
    }
}