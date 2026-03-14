using UnityEngine;
using MoreMountains.Feedbacks;
using System.Collections.Generic;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.transform.parent.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(20, true);
        }
    }
}
