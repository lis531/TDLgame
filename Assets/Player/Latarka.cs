using UnityEngine;

public class Latarka : MonoBehaviour
{
    public GameObject latarka;
    public GameObject latarka1;
    
    void Start()
    {
        latarka.SetActive(false);
        latarka1.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            latarka.SetActive(!latarka.activeSelf);
            latarka1.SetActive(!latarka1.activeSelf);
        }
    }
}