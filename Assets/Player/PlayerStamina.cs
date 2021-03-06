using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    public VolumeProfile MainGlobalVolume;

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
    void Update()
    {
        if(stamina <= 150 && stamina > 60)
        {
            MainGlobalVolume.TryGet<Vignette>(out var vignette);
            MainGlobalVolume.TryGet<ColorAdjustments>(out var colorAdjustments);
            colorAdjustments.postExposure.overrideState = true;
            colorAdjustments.postExposure.value = (stamina / 75 - 2f);
            vignette.intensity.overrideState = true;
            vignette.intensity.value = (1f - stamina / 350 - 0.3f);
        }
        else if (stamina <= 60)
        {
            MainGlobalVolume.TryGet<Vignette>(out var vignette);
            MainGlobalVolume.TryGet<ColorAdjustments>(out var colorAdjustments);
            colorAdjustments.postExposure.overrideState = true;
            vignette.intensity.overrideState = true;
            colorAdjustments.postExposure.value = -1.2f;
            vignette.intensity.value = 0.53f;
        }
        else
        {
            MainGlobalVolume.TryGet<Vignette>(out var vignette);
            MainGlobalVolume.TryGet<ColorAdjustments>(out var colorAdjustments);
            colorAdjustments.postExposure.overrideState = true;
            colorAdjustments.postExposure.value = 0f;
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0.28f;
        }
    }
}