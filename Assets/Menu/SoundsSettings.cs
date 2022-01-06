using UnityEngine;
using UnityEngine.UI;

public class SoundsSettings : MonoBehaviour
{
    Slider sfxSlider;
    Slider musicSlider;

    public void UpdateSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
   
    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    private void Start()
    {
        sfxSlider = transform.GetChild(0).GetComponent<Slider>();
        musicSlider = transform.GetChild(1).GetComponent<Slider>();

        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
}