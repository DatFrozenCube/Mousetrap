using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject settingsWindow;
    public void StartGame()
    {
        CrossfadeController.Instance.Fade(CrossfadeController.FadeType.Scene);
    }

    public void Settings()
    {
        StartCoroutine(OpenSettings());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator OpenSettings()
    {
        transform.parent.gameObject.GetComponent<MenuFader>().FadeMenu(false);
        yield return new WaitForSeconds(.2f);
        settingsWindow.GetComponent<MenuFader>().FadeMenu(true);
    }
}
