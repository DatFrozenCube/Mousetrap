using System;
using EasyTextEffects;
using System.Collections;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public static PointsController Instance;
    public int Points;
    public bool finishedPointCalc = false;

    [SerializeField] private float pointWaitTime = 0.02f;
    [SerializeField] private float pointEndTime = 0.2f;
    [SerializeField] private AudioClip pointIncrease;
    [SerializeField] private AudioClip pointsComplete;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TextEffect pointsTextEffect;
    private TimeController timeController;
    private MMSoundManager soundManager;
    private int pointAddCounter;

    private void Start()
    {
        timeController = gameObject.GetComponent<TimeController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<MMSoundManager>();
        Instance = this;

        if (timeController.includeTime)
        {
            Cheese.CheeseActions += ScoreFinishLevelPointsTimed;
        }

        else
        {
            Cheese.CheeseActions += ScoreFinishLevelPoints;
        }
    }

    private void OnDestroy()
    {
        if (timeController.includeTime)
        {
            Cheese.CheeseActions -= ScoreFinishLevelPointsTimed;
        }

        else
        {
            Cheese.CheeseActions -= ScoreFinishLevelPoints;
        }
    }

    public void ScorePoints(int points, AudioClip sound)
    {
        Points += points;
        soundManager.PlaySound(sound, volume: 0.25f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position);
    }

    private void ScoreFinishLevelPointsTimed()
    {
        StartCoroutine(PointsAnimationTimed(pointWaitTime, pointEndTime));
    }

    private void ScoreFinishLevelPoints()
    {
        StartCoroutine(PointsAnimation(pointEndTime));
    }

    private IEnumerator PointsAnimationTimed(float waitTime, float endTime)
    {
        int finishTime = TimeController.Instance.GetFinishTime();
        while (pointAddCounter < finishTime)
        {
            yield return new WaitForSeconds(waitTime);
            soundManager.PlaySound(pointIncrease, volume: 0.35f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position, pitch: .1f * pointAddCounter);
            Points += 10;
            pointsText.text = Points.ToString();
            pointsTextEffect.Refresh();
            pointAddCounter++;
        }

        yield return new WaitForSeconds(endTime);
        soundManager.PlaySound(pointsComplete, volume: 0.25f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position);
        pointAddCounter = 0;
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Level);
    }

    private IEnumerator PointsAnimation(float endTime)
    {
        ScorePoints(100 * LevelController.LevelNumber, pointIncrease);
        pointsText.text = Points.ToString();
        pointsTextEffect.Refresh();
        yield return new WaitForSeconds(endTime);
        soundManager.PlaySound(pointsComplete, volume: 0.25f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position);
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Level);
    }
}
