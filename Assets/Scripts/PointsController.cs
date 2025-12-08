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
    private MMSoundManager soundManager;
    private int pointAddCounter;

    private void Start()
    {
        Instance = this;
        Cheese.CheeseActions += ScoreFinishLevelPoints;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<MMSoundManager>();
    }

    private void OnDestroy()
    {
        Cheese.CheeseActions -= ScoreFinishLevelPoints;
    }

    public void ScorePoints(int points, AudioClip sound)
    {
        Points += points;
        soundManager.PlaySound(sound, volume: 0.25f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position);
    }

    private void ScoreFinishLevelPoints()
    {
        StartCoroutine(PointsAnimation(pointWaitTime, pointEndTime));
    }

    private IEnumerator PointsAnimation(float waitTime, float endTime)
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
}
