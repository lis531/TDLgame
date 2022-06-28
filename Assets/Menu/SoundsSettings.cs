using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{
    public Slider m_MainSlider;
    public Slider m_SfxSlider;
    public Slider m_AmbientSlider;

    public AudioMixer m_MainMixer;
    public static float[] m_MainVolume;

    private void Start()
    {
        m_MainSlider = transform.GetChild(0).GetComponent<Slider>();
        m_AmbientSlider = transform.GetChild(2).GetComponent<Slider>();
        m_SfxSlider = transform.GetChild(1).GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        m_MainMixer.SetFloat("MasterVolume", m_MainSlider.value);
        m_MainMixer.SetFloat("AmbientVolume", m_AmbientSlider.value);
        m_MainMixer.SetFloat("SFXVolume", m_SfxSlider.value);

        m_MainVolume[0] = m_MainSlider.value;
        m_MainVolume[1] = m_AmbientSlider.value;
        m_MainVolume[2] = m_SfxSlider.value;
    }
}