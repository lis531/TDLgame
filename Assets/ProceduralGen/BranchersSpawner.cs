using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchersSpawner : MonoBehaviour
{
    public GameObject brancher;

    const int maxBranchers = 100;
    int spawnedBranchers = 0;

    bool Chance(int percent)
    {
        int rand = Random.Range(0, 100);

        if(rand < percent)
            return true;
        
        else
            return false;
    }

    public enum Dir
    {
        Left, 
        Right
    };

    public void SpawnBrancher(GameObject relativeToBrancher, Dir direction)
    {
        if (spawnedBranchers < maxBranchers)
        {
            spawnedBranchers++;

            Vector3 spawnPos = relativeToBrancher.transform.position;
            Vector3 dir;

            if (direction == Dir.Left)
            {
                dir = -relativeToBrancher.transform.right;
                spawnPos += relativeToBrancher.transform.forward - relativeToBrancher.transform.right;
            }
            else
            {
                dir = relativeToBrancher.transform.right;
                spawnPos += relativeToBrancher.transform.forward + relativeToBrancher.transform.right;
            }

            Instantiate(brancher, spawnPos, Quaternion.Euler(0, Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg, 0));
        }
    }

    void Start()
    {
        Instantiate(brancher, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
    }
}