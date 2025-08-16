using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class ButtonController : MonoBehaviour
{
    public void StartGame()
    {
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Scene);
    }

    public void Settings()
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
