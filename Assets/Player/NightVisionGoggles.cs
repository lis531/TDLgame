using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class NightVisionGoggles : MonoBehaviour
{
    bool TurnedOn;
    public Volume volume;
    public VolumeProfile NightVisonProfile;
    public VolumeProfile GlobalVolumeProfile;
    void Update()
    {
        if (Input.GetKeyDown("n") && PlayerInventory.hasGoggles && !TurnedOn)
        {
            TurnedOn = true;
        }
        else if (Input.GetKeyDown("n") && TurnedOn)
        {
            TurnedOn = false;
        }
        //if (TurnedOn) load profile NightVision
        //else load profile GlobalVolume
        if (TurnedOn)
        {
            volume.profile = NightVisonProfile;
        }
        else
        {
            volume.profile = GlobalVolumeProfile;
        }

    }
}