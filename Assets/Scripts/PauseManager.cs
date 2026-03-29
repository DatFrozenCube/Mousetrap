using System;
using UnityEngine;
using EasyTextEffects;

public static class PauseManager
{
    public static Action pauseActions;
    public static Action resumeActions;
    public static bool pauseAvailable = true;
    public static string[] animatableObjects = {"Gem"};

    private static void PauseObjectAnimations()
    {
        foreach (string str in animatableObjects)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(str))
            {
                obj.GetComponent<Animator>()?.StartPlayback();
            }
        }
    }

    private static void ResumeObjectAnimations()
    {
        foreach (string str in animatableObjects)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(str))
            {
                obj.GetComponent<Animator>()?.StopPlayback();
            }
        }
    }

    public static void PauseGame()
    {
        if (pauseAvailable)
        {
            pauseActions?.Invoke();
            PauseObjectAnimations();
        }
    }

    public static void ResumeGame()
    {
        if (pauseAvailable)
        {
            resumeActions?.Invoke();
            ResumeObjectAnimations();
        }
    }
}
