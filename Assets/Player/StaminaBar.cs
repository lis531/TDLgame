using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Slider staminaBar;

    void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.value = staminaBar.maxValue = PlayerStamina.instance.maxStamina;
    }

    private void Update()
    {
        staminaBar.value = PlayerStamina.stamina;
    }
}