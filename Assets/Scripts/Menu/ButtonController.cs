using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void StartGame()
    {
        CrossfadeController.Instance.Fade(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
