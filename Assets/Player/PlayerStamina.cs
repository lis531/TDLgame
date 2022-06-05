using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{

    // Po pelnym zmeczeniu gracza cooldown na 1.5s w regeneracji staminy i nie mozna w ogole biegac

    public float maxStamina = 350.0f;

    [HideInInspector]
    public static float stamina;
    public float staminaUsageRate = 100.0f;
    public float staminaRegenRate = 100.0f;


    [Header ("WAŻNE! - \"Exhausted Time\" MUSI być dłuższy od \"Tired Time\"")]
    [Space(10)]

    static public bool staminaEnabled = true;

    public float tiredTime = 0.5f;
    public float exhaustedTime = 1.5f;

    public bool tired = false;
    public bool exhausted = false;

    public static PlayerStamina instance;

    void Start()
    {
        instance = this;
        stamina = maxStamina;
    }

    public void TryRegenStamina()
    {
        if(tired || !staminaEnabled) return;

        stamina += staminaRegenRate * Time.deltaTime;

        if(stamina > maxStamina)
            stamina = maxStamina;
    }
    public void TryConsumeStamina()
    {
        if(exhausted || !staminaEnabled) return;

        stamina -= staminaUsageRate * Time.deltaTime;

        if(stamina < 0)
        {
            stamina = 0;
            TryMakePlayerExhausted();
        }
    }

    IEnumerator StaminaTiredCooldown()
    {
        tired = true;
        yield return new WaitForSeconds(tiredTime);
        tired = false;
    }
    IEnumerator StaminaExhaustedCooldown()
    {   
        exhausted = true;
        yield return new WaitForSeconds(exhaustedTime);
        exhausted = false;
    }

    public void TryMakePlayerTired()
    {
        if(tired || exhausted || !staminaEnabled) return;

        StartCoroutine(StaminaTiredCooldown());
    }
    public void TryMakePlayerExhausted()
    {
        if(tired || exhausted || !staminaEnabled) return;

        StartCoroutine(StaminaTiredCooldown());
        StartCoroutine(StaminaExhaustedCooldown());
    }
}