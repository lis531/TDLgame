using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animation anim;

    bool isOpen = false;

    private void Start()
    {
        anim = transform.GetComponent<Animation>();
    }

    public void Open()
    {
        anim.Stop();
        anim.clip = anim.GetClip("Open");
        anim.Play();

        isOpen = true;
    }
    public void Close()
    {
        anim.Stop();
        anim.clip = anim.GetClip("Close");
        anim.Play();

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
            ChangeState();
    }
}