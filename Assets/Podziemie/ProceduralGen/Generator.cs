using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject brancher;

    public int branchersCount = 10;

    public void GenerateAt(Vector3 pos, Vector3 dir)
    {
        GameObject brancherInstance = Instantiate(brancher, pos, Quaternion.identity);
        brancherInstance.transform.LookAt(brancherInstance.transform.position + dir);

        brancherInstance.GetComponent<BrancherController>().BeginGeneration();
    }
}