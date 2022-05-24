using UnityEngine;

public class Nodes : MonoBehaviour
{
    public static Transform[] nodes;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for(int i = 0; i < transform.childCount; i++)
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            for(int j = 0; j < transform.childCount; j++)
            {
                if(i != j)
                    Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(j).position);
            }
        }

    }
 
    void Start()
    {
        nodes = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
            nodes[i] = transform.GetChild(i).transform;
    }
}
