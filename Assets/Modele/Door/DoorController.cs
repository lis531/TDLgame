using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    Animation anim;
    AudioSource aSource;

    private bool isOpen = false;
    bool playingErrorSound = false;
    public static bool canOpen = false;

    public float time;

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

    public void Door()
    {
        if (PlayerInventory.hasKeycard && canOpen && !anim.isPlaying)
        {
            if(!isOpen)
                StartCoroutine(OpenCoroutine());
            else if(isOpen)
                StartCoroutine(CloseCoroutine());
        }
        else if (!playingErrorSound)
            StartCoroutine(PlayErrorSound());
    }
    
    #region AI
    public bool IsOpen()
    {
        return isOpen;
    }
    public void ForceOpen()
    {
        if(!isOpen)
            StartCoroutine(OpenCoroutine()); 
    }
    #endregion
    IEnumerator PlayErrorSound()
    {
        aSource.clip = doorErrorSound;

        aSource.Stop();
        aSource.Play();

        playingErrorSound = true;

        yield return new WaitForSeconds(0.4f);

        playingErrorSound = false;
    }
    IEnumerator OpenCoroutine()
    {
        canOpen = false;
        anim.Stop();
        anim.clip = anim.GetClip("Open");
        anim.Play();

        aSource.clip = doorOpenSound;
        aSource.Stop();
        aSource.Play();

        isOpen = true;
        yield return new WaitForSeconds(time);
        canOpen = true;
    }
    IEnumerator CloseCoroutine()
    {
        canOpen = false;
        anim.Stop();
        anim.clip = anim.GetClip("Close");
        anim.Play();

        aSource.Stop();
        aSource.Play();

        isOpen = false;
        yield return new WaitForSeconds(time);
        canOpen = true;
    }
}