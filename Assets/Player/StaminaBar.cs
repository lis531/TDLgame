using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    Slider staminaBar;
    
    public float maxStamina = 350.0f;
    public float currentStamina;
    public float staminaUsage = 100.0f;
    public float staminaRegen = 50.0f;
    public bool canRun = true;

    void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.value = staminaBar.maxValue = currentStamina = maxStamina;
    }

    public void UseStamina()
    {
        currentStamina -= (staminaUsage * Time.deltaTime);

        if(currentStamina <= 0)
            StartCoroutine(StaminaCooldown());
    }
    public void RegenStamina()
    {
        currentStamina += staminaRegen * Time.deltaTime;

        if (currentStamina >= maxStamina)
            currentStamina = maxStamina;
    }

    private void Update()
    {
        staminaBar.value = currentStamina;
    }

    IEnumerator StaminaCooldown()
    {
        canRun = false;
        currentStamina = 0;
        yield return new WaitForSeconds(2);
        canRun = true;
    }
}