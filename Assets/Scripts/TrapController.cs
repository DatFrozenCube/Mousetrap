using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(false);
    }

    public void GameOver()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
    }
}
