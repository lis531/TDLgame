using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{
    Slider SfxSlider;
    Slider MusicSlider;
    Slider MainSlider;

    public AudioMixer MainMixer;
    public void SetMainVolume(float Volume)
    {
        MainMixer.SetFloat("MainVolume", Volume);
    }

    public void SetSfxVolume(float Volume)
    {
        MainMixer.SetFloat("SfxVolume", Volume);
    }

    public void SetMusicVolume(float Volume)
    {
        MainMixer.SetFloat("MusicVolume", Volume);
    }

    private void Start()
    {
        MainSlider = transform.GetChild(0).GetComponent<Slider>();
        MusicSlider = transform.GetChild(1).GetComponent<Slider>();
        SfxSlider = transform.GetChild(2).GetComponent<Slider>();
    }
}