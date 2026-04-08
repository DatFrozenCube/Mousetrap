using UnityEngine;
using MoreMountains.Feedbacks;
using NUnit.Framework;

public class Gem : MonoBehaviour
{
    [SerializeField][UnityEngine.Range(50, 300)] private int points = 100;
    [SerializeField][UnityEngine.Range(5, 50)] private int money = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.transform.parent.gameObject.CompareTag("Player"))
        {
            if (MoneyController.Instance.GetMoney() >= MoneyController.Instance.GetCapacity())
            {
                MoneyController.Instance.WarnPlayer(2f, 0.5f);
                return;
            }

            PointsController.Instance.ScorePointsAnimated(points);
            MoneyController.Instance.AddMoney(money);
            gameObject.GetComponent<MMF_Player>().PlayFeedbacks();
        }
    }
}
