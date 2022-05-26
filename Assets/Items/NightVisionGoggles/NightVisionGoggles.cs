using UnityEngine;
using UnityEngine.Rendering;

public class NightVisionGoggles : MonoBehaviour
{
    public static bool TurnedOn;
    public Volume volume;
    public VolumeProfile NightVisonProfile;
    public VolumeProfile GlobalVolumeProfile;
    void Update()
    {
        if (Input.GetKeyDown("n") && PlayerInventory.hasGoggles && !TurnedOn)
        {
            TurnedOn = true;
            volume.profile = NightVisonProfile;
        }
        else if (Input.GetKeyDown("n") && TurnedOn && PlayerInventory.hasGoggles)
        {
            TurnedOn = false;
            volume.profile = GlobalVolumeProfile;
        }
    }
}