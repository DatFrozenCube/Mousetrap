using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void StartGame()
    {
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
