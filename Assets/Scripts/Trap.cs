using UnityEngine;

public class Trap : MonoBehaviour
{
    private TrapController controller;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TrapController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.transform.parent.gameObject.CompareTag("Player"))
        {
            controller.GameOver();
        }
    }
}
