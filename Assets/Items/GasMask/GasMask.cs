using UnityEngine;
using System.Collections;

public class GasMask : MonoBehaviour
{
    public GameObject image;
    bool m_MaskOn = false;
    public static bool PlayerIn;
    bool CoroutineRunning;
    public BoxCollider gas;
    void Start()
    {
        image.transform.localScale = new Vector3(0f, 0f, 0f);
    }
    void Update()
    {
        if (PlayerInventory.hasGasMask && Time.timeScale != 0 && Input.GetKeyDown("g"))
        {
            image.transform.localScale = new Vector3(1f, 1f, 1f);
            if (!m_MaskOn)
            {
                m_MaskOn = true;
                image.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (m_MaskOn)
            {
                m_MaskOn = false;
                image.transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
        if (PlayerIn && !m_MaskOn && !CoroutineRunning)
        {
            StartCoroutine(MaskOff());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerIn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerIn = false;
        }
    }
    IEnumerator MaskOff()
    {
        CoroutineRunning = true;
        Health.health -= 10f;
        yield return new WaitForSeconds(1f);
        CoroutineRunning = false;
    }
}