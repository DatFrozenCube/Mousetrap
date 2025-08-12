using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;

public class Cheese : MonoBehaviour
{
    public Sprite CheeseEaten;
    private Mouse player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.PauseInput();
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        GetComponent<SpriteRenderer>().sprite = CheeseEaten;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MMF_Player>().PlayFeedbacks();
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Level);
    }
}

