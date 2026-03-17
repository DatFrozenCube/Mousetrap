using System;
using UnityEngine;
using MoreMountains.Feedbacks;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private TMP_Text healthText;
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
}
