using UnityEngine;
using TMPro;

using System.Collections;

public class ButtonPanelController : MonoBehaviour
{
    ElevatorController parentElevator;

    public Color defaultColor;
    public Color pressedColor;

    TMP_Text display;

    public bool arrowUp = false;

    void Start()
    {
        parentElevator = gameObject.GetComponentInParent<ElevatorController>();

        display = transform.Find("Display").GetComponent<TMP_Text>();

        if(parentElevator.otherElevator == null)
        {
            display.text = "X";
            display.color = Color.red;
            return;
        }

        display.color = defaultColor;

        if(arrowUp)
            display.transform.localScale = new Vector3(1f, -1f, 1f);
    }

    IEnumerator HighlightDisplay()
    {
        display.color = pressedColor;
        yield return new WaitForSeconds(1f);
        display.color = defaultColor;
    }

    public void PressButton()
    {
        if(!parentElevator.isRiding && parentElevator.otherElevator != null)
        {
            StartCoroutine(HighlightDisplay());
            parentElevator.BeginRideToAnotherLevel();
        }
    }
}