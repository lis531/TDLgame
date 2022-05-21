using UnityEngine;

public class BrancherController : MonoBehaviour
{
    const int maxSteps = 100;
    int steps = 0;

    public Generator generator;

    public GameObject tunnel;
    public GameObject room;
    public GameObject door;
    public GameObject lRight;
    public GameObject lLeft;
    public GameObject t;
    public GameObject x;
    public GameObject end;

    private Transform tunnelsParent;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 0f, 1f, 1f);
    
        Gizmos.DrawSphere(transform.position+(transform.forward*1.5f), 0.22f);
        Gizmos.DrawLine(transform.position, transform.position+(transform.forward*1.5f));
    }

    void SpawnPart(GameObject part, float yRotationOffset = 0)
    {
        GameObject partInstance = Instantiate(part, transform.position, Quaternion.identity);
        partInstance.transform.parent = tunnelsParent;
        partInstance.transform.Rotate(new Vector3(0, transform.rotation.y + 180 + yRotationOffset, 0), Space.Self);
        steps++;
    }

    void SpawnDoor(float xOffset, float zOffset, float yRotationOffset)
    {
        transform.position += (transform.forward * zOffset) + (transform.right * xOffset);
        SpawnPart(door, yRotationOffset);
    }
    void SpawnTunnel()
    {
        SpawnPart(tunnel, 0);
        SpawnDoor(0, 8, -90);
        OnGenerationFinish();
    }
    void SpawnRoom()
    {
        SpawnPart(room, -90);
        OnGenerationFinish();
    }
    void SpawnLRight()
    {
        SpawnPart(lRight, 0);
        SpawnDoor(2, 2, 0);
        OnGenerationFinish();
    }
    void SpawnLLeft()
    {
        SpawnPart(lLeft, 0);
        SpawnDoor(-2, 2, 0);
        OnGenerationFinish();
    }
    void SpawnT()
    {
        SpawnPart(t, 180);
        SpawnDoor(-2, 2, 0);
        SpawnDoor(4, 0, 0);
        OnGenerationFinish();
    }
    void SpawnX()
    {
        SpawnPart(x, -90);
        SpawnDoor(-2, 2, 0);
        SpawnDoor(4, 0, 0);
        SpawnDoor(-2, 2, 90);
        OnGenerationFinish();
    }
    void SpawnEnd()
    {
        SpawnPart(end, -90);
        OnGenerationFinish();
    }

    void OnGenerationFinish()
    {
        Destroy(gameObject);
    }

    public void BeginGeneration()
    {
        tunnelsParent = GameObject.Find("Tunnels").transform;

        int choice = Random.Range(0, 7);

        switch(choice)
        {
            case 0:
                SpawnTunnel();
                break;
            case 1:
                SpawnRoom();
                break;
            case 2:
                SpawnLRight();
                break;
            case 3:
                SpawnLLeft();
                break;
            case 4:
                SpawnT();
                break;
            case 5:
                SpawnX();
                break;
            case 6:
                SpawnEnd();
                break;
            default:
                Debug.LogError("BrancherController::Start() - Invalid choice value: " + choice);
                break;
        }     
    }
}