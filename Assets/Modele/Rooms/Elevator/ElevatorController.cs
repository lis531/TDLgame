using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    Animation anim;
    AudioSource aSource;

    public AudioClip elevatorOpenClose;
    public AudioClip elevatorRide;
    public AudioClip elevatorBeep;

    public ElevatorController otherElevator;

    private Transform elevatorCenter;
    private Transform playerTransform;

    public float elevatorRideTime = 3.0f;

    public bool openByDefault = false;
    bool opened;

    [HideInInspector]
    public bool isRiding = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Transform center = transform.Find("ElevatorCenter").transform;

        Gizmos.DrawWireCube(center.position, center.localScale*2);
    }

    void Start()
    {
        anim = GetComponent<Animation>();
        aSource = GetComponent<AudioSource>();

        playerTransform = GameObject.Find("Player").transform;

        elevatorCenter = transform.Find("ElevatorCenter").transform;

        if(openByDefault)
            OpenElevator();
    }

    void Update()
    {
        if(PlayerInventory.unlocked && !opened)
        {
            openByDefault = true;
            OpenElevator();        
        }
    }
    void RideToAnotherLevel()
    {
        Vector3 diff = playerTransform.position - elevatorCenter.position;

        playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;

        playerTransform.position = otherElevator.elevatorCenter.position + new Vector3(diff.x, 0, diff.z);

        playerTransform.gameObject.GetComponent<CharacterController>().enabled = true;
    }
    bool IsPlayerInside()
    {
        Vector3 pos    = playerTransform.position;
        Vector3 center = elevatorCenter.position;
        Vector3 bounds = elevatorCenter.localScale;

        if(pos.x > center.x + bounds.x || pos.x < center.x - bounds.x)
            return false;

        else if(pos.y > center.y + bounds.y || pos.y < center.y - bounds.y)
            return false;

        else if(pos.z > center.z + bounds.z || pos.z < center.z - bounds.z)
            return false;

        return true;
    }

    IEnumerator ElevatorCoroutine()
    {
        isRiding = true;
        otherElevator.isRiding = true;

        aSource.Play();
        anim.Play("CloseElevator");

        float closeTime = anim.clip.length;

        yield return new WaitForSeconds(closeTime);

        aSource.clip = elevatorRide;
        aSource.loop = true;
        aSource.Play();

        yield return new WaitForSeconds(elevatorRideTime);

        if(IsPlayerInside())
            RideToAnotherLevel();

        aSource.clip = elevatorBeep;
        aSource.loop = false;
        aSource.Play();

        yield return new WaitForSeconds(0.25f);

        otherElevator.OpenElevator();

        yield return new WaitForSeconds(closeTime);

        isRiding = false;
        otherElevator.isRiding = false;
    }

    public void OpenElevator()
    {
        aSource.loop = false;
        aSource.clip = elevatorOpenClose;

        aSource.Play();
        anim.Play("OpenElevator");
    }
    public void BeginRideToAnotherLevel()
    {
        if(otherElevator is not null && !isRiding)
            StartCoroutine(ElevatorCoroutine());
    }
}