using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    #region Quality Levels
    private Toggle highToggle;
    private Toggle mediumToggle;
    private Toggle lowToggle;
    private GameObject blocker;
    public GameObject graphicalSettings;

    public void CheckSetting(Toggle toggle)
    {
        if(toggle.isOn)
        {
            if (toggle.gameObject.name == "High")
            {
                blocker.transform.position = highToggle.transform.GetChild(0).position;
                highToggle.isOn = true;
                mediumToggle.isOn = false;
                lowToggle.isOn = false;
                CheckVSyncToggle();
                QualitySettings.SetQualityLevel(0);
            }
            else if (toggle.gameObject.name == "Medium")
            {
                blocker.transform.position = mediumToggle.transform.GetChild(0).position;
                highToggle.isOn = false;
                mediumToggle.isOn = true;
                lowToggle.isOn = false;
                CheckVSyncToggle();
                QualitySettings.SetQualityLevel(1);
            }
            else if (toggle.gameObject.name == "Low")
            {
                blocker.transform.position = lowToggle.transform.GetChild(0).position;
                highToggle.isOn = false;
                mediumToggle.isOn = false;
                lowToggle.isOn = true;
                CheckVSyncToggle();
                QualitySettings.SetQualityLevel(2);
            }
        }
    }
    #endregion

    #region VSync
    Toggle vSyncToggle;

    public void CheckVSyncToggle()
    {
        if (vSyncToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }
    #endregion

    void Start()
    {
        highToggle = graphicalSettings.transform.GetChild(0).GetComponent<Toggle>();
        mediumToggle  = graphicalSettings.transform.GetChild(1).GetComponent<Toggle>();
        lowToggle  = graphicalSettings.transform.GetChild(2).GetComponent<Toggle>();
        blocker       = graphicalSettings.transform.GetChild(3).gameObject;
        vSyncToggle   = transform.GetChild(1).GetComponent<Toggle>();
    }
}