using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    public int DefaultTime = 60;

    private int finishTime = 0;
    private int timer = 60;
    
    [SerializeField] private TMP_Text timerText;

    public int GetFinishTime()
    {
        finishTime = timer;
        return finishTime;
    }

    private void Start()
    {
        Instance = this;

        LevelController.LevelActions += ResetTime;
        Cheese.CheeseActions += StopTime;
        ResetTime();
    }

    private void OnDestroy()
    {
        LevelController.LevelActions -= ResetTime;
        Cheese.CheeseActions -= StopTime;
    }

    private void StopTime()
    {
        StopAllCoroutines();
        Debug.Log("Countdown stopped");
    }

    private void ResetTime()
    {
        if (LevelController.LevelNumber == 1)
        {
            timer = DefaultTime;
            finishTime = DefaultTime;
            StartCoroutine(Countdown());
            Debug.Log("Starting countdown");
        }

        else
        {
            timer = DefaultTime + (int)Mathf.Pow(LevelController.LevelNumber - 2, 2);
            finishTime = timer;
            StartCoroutine(Countdown());
            Debug.Log("Starting countdown");
        }
    }

    private IEnumerator Countdown()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = $"Time: {timer.ToString()}";
        }

        TrapController.Instance.GameOver();
    }
}
