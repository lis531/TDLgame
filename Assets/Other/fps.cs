using UnityEngine;
using TMPro;

public class fps : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Time.frameCount % 60 == 0)
            {
                int fps = (int)(1f / Time.unscaledDeltaTime);
                GetComponent<TextMeshProUGUI>().text = "FPS: " + fps.ToString("F0");
            }
        }
    }
}