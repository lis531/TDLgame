using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    public GameObject saveButton;

    public void Load()
    {
        SoundsSettings.m_MainMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        SoundsSettings.m_MainMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        SoundsSettings.m_MainMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        GraphicsSettings.FullScreenToogle = PlayerPrefs.GetInt("FullScreen");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", SoundsSettings.m_MainSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", SoundsSettings.m_MusicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SoundsSettings.m_SFXSlider.value);
        PlayerPrefs.SetInt("FullScreen", (int)GraphicsSettings.FullScreenToogle);
        PlayerPrefs.SetInt("Quality", (int)QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("Vsync", (int)QualitySettings.vSyncCount);
    }
}