using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrancherController : MonoBehaviour
{
    const int maxSteps = 100;

    public GameObject Tunnel;
    public GameObject Room;
    public GameObject LRight;
    public GameObject LLeft;
    public GameObject T;
    public GameObject X;
    public GameObject End;

    GameObject previouslySpawned = null;
    GameObject spawnedPrefab = null;

    Collider col;

    BranchersSpawner bs;

    private int MoveOffset()
    {
        if (previouslySpawned != null)
        {
            // Jezeli to tunel
            if (previouslySpawned == Tunnel)
                return 8;

            // Jezeli to pokoj
            else if (previouslySpawned == Room)
                return 16;

            // Jezeli to jakies rozwidlenie
            else
                return 2;

        }

        return 0;
    }
    private void MoveBrancher()
    {
        transform.Translate(Vector3.forward * MoveOffset());
    }
    private void MoveBrancherLeft()
    {
        transform.Translate(Vector3.forward);
        transform.Translate(Vector3.left);
        transform.Rotate(new Vector3(0, -90, 0));
    }
    private void MoveBrancherRight()
    {
        transform.Translate(Vector3.forward);
        transform.Translate(Vector3.right);
        transform.Rotate(new Vector3(0, 90, 0));
    }

    public void SpawnTunnel()
    {
        spawnedPrefab = Instantiate(Tunnel, transform.position, transform.rotation);
        previouslySpawned = Tunnel;

        MoveBrancher();

        spawnedPrefab.GetComponent<Collider>().isTrigger = false;
    }
    private void SpawnRoom()
    {
        spawnedPrefab = Instantiate(Room, transform.position, transform.rotation);
        previouslySpawned = Room;

        MoveBrancher();

        spawnedPrefab.GetComponent<Collider>().isTrigger = false;

        SpawnTunnel();
    }
    private void SpawnLRight()
    {
        spawnedPrefab = Instantiate(LRight, transform.position, transform.rotation);
        previouslySpawned = LRight;

        MoveBrancherRight();

        spawnedPrefab.GetComponent<Collider>().isTrigger = false;

        SpawnTunnel();
    }
    private void SpawnLLeft()
    {
        spawnedPrefab = Instantiate(LLeft, transform.position, transform.rotation);
        previouslySpawned = LLeft;

        MoveBrancherLeft();

        spawnedPrefab.GetComponent<Collider>().isTrigger = false;

        SpawnTunnel();
    }
    private void SpawnT()
    {
        spawnedPrefab =  Instantiate(T, transform.position, transform.rotation);
        previouslySpawned = T;

        bs.SpawnBrancher(gameObject, BranchersSpawner.Dir.Left);

        MoveBrancherRight();

        spawnedPrefab.GetComponent<Collider>().isTrigger = false;

        SpawnTunnel();
    }
    private void SpawnX()
    {
        spawnedPrefab = Instantiate(X, transform.position, transform.rotation);
        previouslySpawned = X;

        bs.SpawnBrancher(gameObject, BranchersSpawner.Dir.Left);
        bs.SpawnBrancher(gameObject, BranchersSpawner.Dir.Right);

        MoveBrancher();

        spawnedPrefab.GetComponent<Collider>().isTrigger = false;

        SpawnTunnel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("TunnelPart"))
        {
            Instantiate(End, transform.position, transform.rotation);
            Destroy(gameObject);
            Debug.Log("Col");
        }
    }

    void SpawnRandomSegment()
    {
        int rand = Random.Range(0, 20);

        switch(rand)
        {
            case 0:
                SpawnTunnel();
                break;
            case 1:
                SpawnTunnel();
                break;
            case 2:
                SpawnTunnel();
                break;
            case 3:
                SpawnTunnel();
                break;
            case 4:
                SpawnTunnel();
                break;
            case 5:
                SpawnTunnel();
                break;
            case 6:
                SpawnTunnel();
                break;
            case 7:
                SpawnLLeft();
                break;
            case 8:
                SpawnLLeft();
                break;
            case 9:
                SpawnLLeft();
                break;
            case 10:
                SpawnLRight();
                break;
            case 11:
                SpawnLRight();
                break;
            case 12:
                SpawnLRight();
                break;
            case 13:
                SpawnT();
                break;
            case 14:
                SpawnT();
                break;
            case 15:
                SpawnX();
                break;
            case 16:
                SpawnX();
                break;
            case 17:
                SpawnRoom();
                break;
            case 18:
                SpawnRoom();
                break;
            case 19:
                SpawnRoom();
                break;
        }

        previouslySpawned.GetComponent<Collider>().isTrigger = false;
    }


    IEnumerator test()
    {
        for (int steps = 0; steps < maxSteps; steps++)
        {
            SpawnRandomSegment();
            yield return new WaitForSeconds(0.2f);
        }

        Instantiate(End, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Awake()
    {
        bs = GameObject.Find("ProceduralGenerator").GetComponent<BranchersSpawner>();
        col = transform.GetComponent<Collider>();

        SpawnTunnel();

        StartCoroutine(test());
    }
}