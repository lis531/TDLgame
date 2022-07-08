using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveSettings : MonoBehaviour
{
    public GameObject saveButton;
    public Slider m_MainSlider;
    public Slider m_SFXSlider;
    public Slider m_MusicSlider;
    public AudioMixer m_MainMixer;
    void Start()
    {
        Load();
    }
    public void Load()
    {
        m_MainMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        m_MainMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        m_MainMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        GraphicsSettings.FullScreenToogle = PlayerPrefs.GetInt("FullScreen");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("SFXVolume", m_SFXSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", m_MusicSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", m_MainSlider.value);
        PlayerPrefs.SetInt("FullScreen", (int)GraphicsSettings.FullScreenToogle);
        PlayerPrefs.SetInt("Quality", (int)QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("Vsync", (int)QualitySettings.vSyncCount);
    }
}