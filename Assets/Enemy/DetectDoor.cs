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
        {
            GameObject door = hit.collider.transform.parent.gameObject;
            DoorController controller = door.GetComponent<DoorController>();
            if(controller != null)
            {
                if(!controller.IsOpen())
                    return door;
            }
        }
        return null;
    }
}