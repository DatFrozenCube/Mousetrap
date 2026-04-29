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
    public float PointsMultiplier = 1f;
    public bool FinishedPointCalc = false;

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

        PauseManager.pauseActions += pointsTextEffect.StopAllEffects;
        PauseManager.resumeActions += pointsTextEffect.StartOnStartEffects;
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

        PauseManager.pauseActions -= pointsTextEffect.StopAllEffects;
        PauseManager.resumeActions -= pointsTextEffect.StartOnStartEffects;
    }

    public void ScorePoints(int points, AudioClip sound)
    {
        Points += points;
        soundManager.PlaySound(sound, volume: 0.25f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position);
    }

    public void ScorePointsAnimated(int points)
    {
        StartCoroutine(PointsAnimationTimed(pointWaitTime, pointEndTime, points / 10));
    }

    private void ScoreFinishLevelPointsTimed()
    {
        StartCoroutine(PointsAnimationTimed(pointWaitTime, pointEndTime, TimeController.Instance.GetFinishTime(), true));
    }

    private void ScoreFinishLevelPoints()
    {
        StartCoroutine(PointsAnimation(pointEndTime));
    }

    private IEnumerator PointsAnimationTimed(float waitTime, float endTime, int finishTime, bool endLevel = false)
    {
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

        if (endLevel)
        {
            CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Level);
        }
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
