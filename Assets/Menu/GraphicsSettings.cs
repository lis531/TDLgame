using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    #region Quality Levels
    private Toggle highestToggle;
    private Toggle mediumToggle;
    private Toggle lowestToggle;

    private GameObject blocker;

    public GameObject graphicalSettings;

    public void CheckSetting(Toggle toggle)
    {
        if(toggle.isOn)
        {
            if (toggle.gameObject.name == "Highest")
                SetQuality(Setting.Highest);
            else if (toggle.gameObject.name == "Medium")
                SetQuality(Setting.Medium);
            else if (toggle.gameObject.name == "Lowest")
                SetQuality(Setting.Lowest);
            else
                Debug.LogWarning("Wrong Toggle name! \"" + toggle + "\"");
        }
    }

    enum Setting
    {
        Highest,
        Medium,
        Lowest
    };

    void SetQuality(Setting qualityLevel)
    {
        switch(qualityLevel)
        { 
            case Setting.Highest:
                blocker.transform.position = highestToggle.transform.GetChild(0).position;
                highestToggle.isOn = true;
                mediumToggle.isOn = false;
                lowestToggle.isOn = false;

                PlayerPrefs.SetInt("GraphicsLevel", 2);
                break;

            case Setting.Medium:
                blocker.transform.position = mediumToggle.transform.GetChild(0).position;
                highestToggle.isOn = false;
                mediumToggle.isOn = true;
                lowestToggle.isOn = false;

                PlayerPrefs.SetInt("GraphicsLevel", 1);
                break;

            case Setting.Lowest:
                blocker.transform.position = lowestToggle.transform.GetChild(0).position;
                highestToggle.isOn = false;
                mediumToggle.isOn = false;
                lowestToggle.isOn = true;

                PlayerPrefs.SetInt("GraphicsLevel", 0);
                break;

            default: break;
        }
    }
    #endregion

    #region VSync
    Toggle vSyncToggle;

    public void CheckVSyncToggle()
    {
        if (vSyncToggle.isOn)
            PlayerPrefs.SetInt("VSyncLevel", 1);
        else
            PlayerPrefs.SetInt("VSyncLevel", 0);
    }
    #endregion

    void Start()
    {
        highestToggle = graphicalSettings.transform.GetChild(0).GetComponent<Toggle>();
        mediumToggle  = graphicalSettings.transform.GetChild(1).GetComponent<Toggle>();
        lowestToggle  = graphicalSettings.transform.GetChild(2).GetComponent<Toggle>();
        blocker       = graphicalSettings.transform.GetChild(3).gameObject;
        vSyncToggle   = transform.GetChild(1).GetComponent<Toggle>();
        
        switch (PlayerPrefs.GetInt("GraphicsLevel"))
        {
            case 0:
                SetQuality(Setting.Lowest);
                break;
            case 1:
                SetQuality(Setting.Medium);
                break;
            case 2:
                SetQuality(Setting.Highest);
                break;
        }

        if (PlayerPrefs.GetInt("VSyncLevel") == 1)
            vSyncToggle.isOn = true;
        else
            vSyncToggle.isOn = false;
    }
}