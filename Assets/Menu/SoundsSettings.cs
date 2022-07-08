using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{
    public Slider m_MainSlider;
    public Slider m_SFXSlider;
    public Slider m_MusicSlider;
    public AudioMixer m_MainMixer;
    private void Start()
    {
        m_MainSlider = transform.GetChild(0).GetComponent<Slider>();
        m_SFXSlider = transform.GetChild(1).GetComponent<Slider>();
        m_MusicSlider = transform.GetChild(2).GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        m_MainMixer.SetFloat("SFXVolume", m_SFXSlider.value);
        m_MainMixer.SetFloat("MusicVolume", m_MusicSlider.value);
        m_MainMixer.SetFloat("MasterVolume", m_MainSlider.value);
    }
}