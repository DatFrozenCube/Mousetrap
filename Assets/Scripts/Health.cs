using System;
using UnityEngine;
using MoreMountains.Feedbacks;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using EasyTextEffects;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image animatedHeart;
    private float currentHealth;
    private float healthPercentage;
    private MMF_Player mmfPlayer;

    private void Start()
    {
        currentHealth = maxHealth;
        healthPercentage = 1f;
        mmfPlayer = gameObject.GetComponent<MMF_Player>();

        if (mmfPlayer != null )
        {
            mmfPlayer = gameObject.GetComponentInChildren<MMF_Player>();
        }

        PauseManager.pauseActions += healthText.gameObject.GetComponent<TextEffect>().StopAllEffects;
        PauseManager.pauseActions += ToggleOffHeartAnimation;
        PauseManager.resumeActions += healthText.gameObject.GetComponent<TextEffect>().StartOnStartEffects;
        PauseManager.resumeActions += ToggleOnHeartAnimation;
    }

    private void OnDestroy()
    {
        PauseManager.pauseActions -= healthText.gameObject.GetComponent<TextEffect>().StopAllEffects;
        PauseManager.resumeActions -= healthText.gameObject.GetComponent<TextEffect>().StartOnStartEffects;
        PauseManager.pauseActions -= ToggleOffHeartAnimation;
        PauseManager.resumeActions -= ToggleOnHeartAnimation;
    }

    private void ToggleOnHeartAnimation()
    {
        if (animatedHeart != null)
        {
            animatedHeart.material.SetInteger("_Animate", 1);
        }
    }

    private void ToggleOffHeartAnimation()
    {
        if (animatedHeart != null)
        {
            animatedHeart.material.SetInteger("_Animate", 0);
        }
    }

    public void TakeDamage(float damage, bool playFeedbacks)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthPercentage = (currentHealth / maxHealth) * 100;

        if (mmfPlayer != null && playFeedbacks)
        {
            mmfPlayer.PlayFeedbacks();
        }

        if (healthPercentage > 0)
        {
            healthText.text = $"{healthPercentage}%";
        }

        else
        {
            TrapController.Instance.GameOver();
        }
    }

    public void Heal(float healAmount, bool playFeedbacks)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthPercentage = (currentHealth / maxHealth) * 100;

        if (mmfPlayer != null && playFeedbacks) 
        {
            mmfPlayer.PlayFeedbacks();
        }

        healthText.text = $"{healthPercentage}%";
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        Heal(newMaxHealth, false);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
