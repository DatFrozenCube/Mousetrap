using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using MoreMountains;
using Lofelt.NiceVibrations;
using MoreMountains.Tools;

public class ButtonController : MonoBehaviour
{
    public void StartGame()
    {
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Scene);
    }

    public void ToggleMute(string track)
    {
        if (track.ToLower() == "master")
        {
            Audio.Instance.ToggleMaster();
        }

        else if (track.ToLower() == "sfx")
        {
            Audio.Instance.ToggleSFX();
        }
    }

    public void Next()
    {
        TitleStart.Instance.UpdateMenu(true);
    }

    public void Back()
    {
        TitleStart.Instance.UpdateMenu(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
