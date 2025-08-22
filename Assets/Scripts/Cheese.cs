using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;

public class Cheese : MonoBehaviour
{
    public Sprite CheeseEaten;
    [SerializeField] private ParticleSystem cheeseParticles;
    private MMF_ParticlesInstantiation cheeseParticlesFeedback;
    private Mouse player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();

        if (!BasicSettings.Instance.IsParticlesOn)
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
        player.PauseInput();
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        GetComponent<SpriteRenderer>().sprite = CheeseEaten;
        GetComponent<Collider2D>().enabled = false;

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
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Level);
    }
}

