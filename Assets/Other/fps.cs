using UnityEngine;
using TMPro;

public class fps : MonoBehaviour
{
    void Update()
    {
        //when i press F1 start saveing all fps and if I press F1, it will show the average fps
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Time.frameCount == 1)
            {
                GetComponent<TextMeshProUGUI>().text = "FPS: " + Time.frameCount;
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = "FPS: " + (Time.frameCount / Time.time);
            }
        }
    }
}