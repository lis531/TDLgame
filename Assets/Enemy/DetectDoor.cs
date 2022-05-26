using UnityEngine;

public class DetectDoor : MonoBehaviour
{
    public LayerMask m_DoorLayer;
    public float m_DetectRange;
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * m_DetectRange);
    }
    public GameObject CollidedWithDoor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, m_DetectRange, m_DoorLayer))
            return hit.collider.transform.parent.gameObject;
        return null;
    }
}