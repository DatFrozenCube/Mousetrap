using UnityEditor.Rendering;
using System;
using UnityEngine;
using MoreMountains.Feedbacks;
using NUnit.Framework;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private MMF_Feedback[] damageFeedbacks;
    [SerializeField] private MMF_Feedback[] healFeedbacks;
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
        healthPercentage = currentHealth / maxHealth;

        if (mmfPlayer != null && playFeedbacks)
        {
            foreach (var feedback in damageFeedbacks)
            {
                mmfPlayer.AddFeedback(feedback);
            }
            mmfPlayer.PlayFeedbacks();
            mmfPlayer.FeedbacksList.Clear();
        }
    }

    public void Heal(float healAmount, bool playFeedbacks)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthPercentage = currentHealth / maxHealth;

        if (mmfPlayer != null && playFeedbacks) 
        {
            foreach (var feedback in healFeedbacks)
            {
                mmfPlayer.AddFeedback(feedback);
            }
            mmfPlayer.PlayFeedbacks();
            mmfPlayer.FeedbacksList.Clear();
        }
    }
}
