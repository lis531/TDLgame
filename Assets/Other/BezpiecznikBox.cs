using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezpiecznikBox : MonoBehaviour
{
    static int number;
    void Start()
    {
        int number = Random.Range(1, 3);
        Debug.Log(number);
    }
    public static void Add()
    {
        number = number + 1;
        Debug.Log(number);
        if (number == 4)
        {
            DoorController.canOpen = true;
            Debug.Log("Otwieranie drzwi");
        }
    }
}
