using UnityEngine;
using UnityEngine.UI;

public class SoundsSettings : MonoBehaviour
{
    public void UpdateSFXVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
    }
   
    public void UpdateMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }
}