using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioSource m_AudioSource0;
    public AudioSource m_AudioSource1;

    public AudioClip m_MenuMusic;

    void Start()
    {
        PlayAudio0();
    }

    void PlayAudio0()
    {
        m_AudioSource0.Play();
        Invoke("PlayAudio1", m_MenuMusic.length-1.8f);
    }
    void PlayAudio1()
    {
        m_AudioSource1.Play();
        Invoke("PlayAudio0", m_MenuMusic.length-1.8f);
    }
}
