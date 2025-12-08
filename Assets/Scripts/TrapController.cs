using System.Collections;
using MoreMountains.Tools;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public static TrapController Instance;

    [SerializeField] private GameObject canvas;

    private void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        canvas.SetActive(false);
    }

    public void GameOver()
    {
        canvas.SetActive(true);
        MMSoundManager.Instance.StopAllSounds();
        Time.timeScale = 0;
    }
}
