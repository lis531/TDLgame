using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animation anim;
    AudioSource aSource;
    GameObject player;

    public bool useGenerator = false;
    bool wasUsed = false;

    Generator generator;

    bool isOpen = false;

    const float interactionDistance = 2.0f;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 1f);

        Gizmos.DrawSphere(transform.position + (transform.right * 2), 0.22f);
        Gizmos.DrawLine(transform.position, transform.position+ (transform.right * 2));
    }

    private void Start()
    {
        if(useGenerator)
            generator = GameObject.Find("Generator").GetComponent<Generator>();

        anim = transform.GetComponent<Animation>();
        aSource = transform.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Open()
    {
        if(useGenerator && !wasUsed)
            generator.GenerateAt(transform.position, transform.right);

        anim.Stop();
        anim.clip = anim.GetClip("Open");
        anim.Play();

        aSource.Stop();
        aSource.Play();

        wasUsed = true;
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