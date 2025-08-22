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

    public void ToggleParticleSystem()
    {
        BasicSettings.Instance.ToggleParticles();
        Audio.Instance.ButtonClick();
    }

    public void ToggleMute(string track)
    {
        if (track.ToLower() == "master")
        {
            AudioSettings.Instance.ToggleMaster();
        }

        else if (track.ToLower() == "sfx")
        {
            AudioSettings.Instance.ToggleSFX();
        }

        else if (track.ToLower() == "music")
        {
            AudioSettings.Instance.ToggleMusic();
        }

            Audio.Instance.ButtonClick();
    }

    public void Next()
    {
        TitleStart.Instance.UpdateMenu(true);
        Audio.Instance.ButtonClick();
    }

    public void Back()
    {
        TitleStart.Instance.UpdateMenu(false);
        Audio.Instance.ButtonClick();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
