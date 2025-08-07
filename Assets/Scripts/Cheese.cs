using UnityEngine;

public class Cheese : MonoBehaviour
{
    private Mouse player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.PauseInput();
        CrossfadeController.Instance.Fade();
    }
}

