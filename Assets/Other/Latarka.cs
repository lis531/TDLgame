using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.T) && latarka.activeSelf == false && latarka1.activeSelf == false)
        {
            latarka.SetActive(true);
            latarka1.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.T) && latarka.activeSelf == true && latarka1.activeSelf == true)
        {
            latarka.SetActive(false);
            latarka1.SetActive(false);
        }
    }
}