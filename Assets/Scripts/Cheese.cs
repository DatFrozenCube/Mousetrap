using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;
using System;

public class Cheese : MonoBehaviour
{
    public Sprite CheeseEaten;
    public static Action CheeseActions;
    [SerializeField] private ParticleSystem cheeseParticles;
    [SerializeField] private AudioClip cheeseScoreSound;
    private MMF_ParticlesInstantiation cheeseParticlesFeedback;
    private Mouse player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();

        if (BasicSettings.Instance != null && !BasicSettings.Instance.IsParticlesOn)
        {
            GetComponent<MMF_Player>().GetFeedbackOfType<MMF_ParticlesInstantiation>();
        }

        else if (GetComponent<MMF_Player>().GetFeedbackOfType<MMF_ParticlesInstantiation>() == null)
        {
            cheeseParticlesFeedback = new MMF_ParticlesInstantiation();
            cheeseParticlesFeedback.ParticlesPrefab = cheeseParticles;
            cheeseParticlesFeedback.ParentTransform = transform;
            GetComponent<MMF_Player>().AddFeedback(cheeseParticlesFeedback);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = CheeseEaten;
        GetComponent<Collider2D>().enabled = false;
        PointsController.Instance.ScorePoints(100, cheeseScoreSound);

        PauseManager.pauseAvailable = false;
        CheeseActions.Invoke();

        if (!BasicSettings.Instance.IsParticlesOn)
        {
            GetComponent<MMF_Player>().RemoveFeedback(1);
        }

        else if (GetComponent<MMF_Player>().GetFeedbackOfType<MMF_ParticlesInstantiation>() == null)
        {
            cheeseParticlesFeedback = new MMF_ParticlesInstantiation();
            cheeseParticlesFeedback.ParticlesPrefab = cheeseParticles;
            cheeseParticlesFeedback.ParentTransform = transform;
            GetComponent<MMF_Player>().AddFeedback(cheeseParticlesFeedback);
        }

        GetComponent<MMF_Player>().PlayFeedbacks();
    }
}

