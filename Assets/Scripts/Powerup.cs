using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float speedBoostAmount = 2f;
    [SerializeField] private float boostDuration = 4f;

    public void ApplyPowerUp(Mouse player)
    {
        player.StartCoroutine(ApplySpeedBoost(player));
    }

    private System.Collections.IEnumerator ApplySpeedBoost(Mouse player)
    {
        player.moveSpeed += speedBoostAmount;
        yield return new WaitForSeconds(boostDuration);
        player.moveSpeed -= speedBoostAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyPowerUp(collision.GetComponent<Mouse>());
        }
    }
}
