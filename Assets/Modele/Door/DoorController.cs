using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    Animation anim;
    AudioSource aSource;

    bool isOpen = false;
    bool playingErrorSound = false;

    public float doorOpenTime = 3.0f;

    public AudioClip doorOpenSound;
    public AudioClip doorErrorSound;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 1f);

        Gizmos.DrawSphere(transform.position + (transform.right * 2), 0.22f);
        Gizmos.DrawLine(transform.position, transform.position+ (transform.right * 2));
    }

    void Start()
    {
        anim = transform.GetComponent<Animation>();
        aSource = transform.GetComponent<AudioSource>();
    }

    IEnumerator OpenCoroutine()
    {
        anim.Stop();
        anim.clip = anim.GetClip("Open");
        anim.Play();

        aSource.clip = doorOpenSound;
        aSource.Stop();
        aSource.Play();

        isOpen = true;

        yield return new WaitForSeconds(doorOpenTime);

        Close();
    }
    void Close()
    {
        anim.Stop();
        anim.clip = anim.GetClip("Close");
        anim.Play();

        aSource.Stop();
        aSource.Play();

        isOpen = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    IEnumerator PlayErrorSound()
    {
        aSource.clip = doorErrorSound;

        aSource.Stop();
        aSource.Play();

        playingErrorSound = true;

        yield return new WaitForSeconds(0.4f);

        playingErrorSound = false;
    }

public void ForceOpen()
    {
        if(!isOpen)
            StartCoroutine(OpenCoroutine());  
    }

    public void Open()
    {
        if(!isOpen && !playingErrorSound)
        {
            if(PlayerInventory.hasKeycard)
                StartCoroutine(OpenCoroutine());
            else
                StartCoroutine(PlayErrorSound());
            
        }

    }
}