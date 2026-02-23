using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CrossfadeController : MonoBehaviour
{
    public static CrossfadeController Instance;
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 2f;
    [SerializeField] private LevelUIUpdater uiUpdater;
    [SerializeField] private MazeVisualizer mazeVisualizer;
    [SerializeField] private RoomFirstMazeGenerator mazeGenerator;
    private Mouse player;

    public enum FadeType
    {
        Level, Scene
    };

    private void Awake()
    {
        Instance = this;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();
        }
    }

    public void Fade(FadeType fadeType)
    {
        switch (fadeType)
        {
            case FadeType.Level:
                bool includeText;
                if (LevelController.LevelNumber > 0)
                {
                    includeText = true;
                }

                else
                {
                    includeText = false;
                }

                StartCoroutine(NextLevel(includeText));
                break;

            case FadeType.Scene:
                StartCoroutine(FadeScene());
                break;
        }
    }
    
    IEnumerator NextLevel(bool includeText)
    {
        LevelController.LevelNumber++;
        uiUpdater.UpdateUI(includeText);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        mazeVisualizer.Clear();
        LevelController.LevelActions.Invoke();
        mazeGenerator.GenerateMaze();
        transition.SetTrigger("End");
        player.UnpauseInput();
    }

    IEnumerator FadeScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);
    }
}
