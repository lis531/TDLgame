using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{
    public static Slider m_MainSlider;
    public static Slider m_SFXSlider;
    public static Slider m_MusicSlider;
    public static AudioMixer m_MainMixer;
    private void Start()
    {
        m_MainSlider = GameObject.Find("MainSlider").GetComponent<Slider>();
        m_SFXSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        m_MusicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        m_MainMixer = Resources.Load("MainMixer") as AudioMixer;
        m_MainSlider = transform.GetChild(0).GetComponent<Slider>();
        m_SFXSlider = transform.GetChild(1).GetComponent<Slider>();
        m_MusicSlider = transform.GetChild(2).GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        m_MainMixer.SetFloat("Master", m_MainSlider.value);
        m_MainMixer.SetFloat("Music", m_MusicSlider.value);
        m_MainMixer.SetFloat("SFX", m_SFXSlider.value);
    }
}