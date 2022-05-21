using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    Animation anim;
    AudioSource aSource;

    public AudioClip elevatorOpenClose;
    public AudioClip elevatorRide;

    public ElevatorController otherElevator;

    private Transform elevatorCenter;
    private Transform playerTransform;

    public float elevatorDoorsWaitTime = 3.0f;
    public float elevatorRideTime = 3.0f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Transform center = transform.Find("ElevatorCenter").transform;

        Gizmos.DrawWireCube(center.position, center.localScale*2);

        Gizmos.DrawSphere(center.position + (center.forward * 2), 0.22f);
        Gizmos.DrawLine(center.position, center.position+(center.forward * 2));
    }

    void Start()
    {
        anim = GetComponent<Animation>();
        aSource = GetComponent<AudioSource>();

        playerTransform = GameObject.Find("Player").transform;

        elevatorCenter = transform.Find("ElevatorCenter").transform;

        if(otherElevator == null)
             anim.Play("OpenElevator");
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
        aSource.loop = false;
        aSource.clip = elevatorOpenClose;

        aSource.Play();
        anim.Play("OpenElevator");

        yield return new WaitForSeconds(anim.clip.length);

        yield return new WaitForSeconds(elevatorDoorsWaitTime);

        aSource.Play();
        anim.Play("CloseElevator");

        yield return new WaitForSeconds(anim.clip.length);

        aSource.clip = elevatorRide;
        aSource.loop = true;
        aSource.Play();

        yield return new WaitForSeconds(elevatorRideTime);

        if(IsPlayerInside())
            RideToAnotherLevel();

        aSource.Stop();
    }

    public void OpenElevator()
    {
        if(otherElevator is not null)
            StartCoroutine(ElevatorCoroutine());
    }
}
