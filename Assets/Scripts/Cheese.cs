using UnityEngine;

public class Cheese : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CrossfadeController.Instance.Fade();
    }
}

