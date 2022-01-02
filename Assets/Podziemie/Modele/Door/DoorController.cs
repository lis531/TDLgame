using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animation anim;
    AudioSource aSource;
    GameObject player;

    bool isOpen = false;

    const float interactionDistance = 2.0f;

    private void Start()
    {
        anim = transform.GetComponent<Animation>();
        aSource = transform.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Open()
    {
        anim.Stop();
        anim.clip = anim.GetClip("Open");
        anim.Play();

        aSource.Stop();
        aSource.Play();

        isOpen = true;
    }
    public void Close()
    {
        anim.Stop();
        anim.clip = anim.GetClip("Close");
        anim.Play();

        aSource.Stop();
        aSource.Play();

        isOpen = false;
    }

    public void ChangeState()
    {
        if(!anim.isPlaying)
        {
            if (isOpen)
                Close();
            else
                Open();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
            if(Vector3.Distance(transform.position, player.transform.position) < interactionDistance &&
               Vector3.Dot(player.transform.forward, (transform.position - player.transform.position).normalized) > 0.65)
                ChangeState();
    }
}