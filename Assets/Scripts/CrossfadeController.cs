using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeController : MonoBehaviour
{
    public static CrossfadeController Instance;
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 2f;
    [SerializeField] private LevelUIUpdater uiUpdater;
    private Mouse player;

    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();
    }

    public void Fade(bool isNextLevel)
    {
        if (isNextLevel)
        {
            StartCoroutine(NextLevel());
        }

        else
        {
            StartCoroutine(FadeTrigger());
        }
    }
    
    IEnumerator NextLevel()
    {
        LevelController.LevelNumber++;
        uiUpdater.UpdateUI();
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        LevelController.DestroyLevel();
        LevelController.LevelActions.Invoke();
        transition.SetTrigger("End");
    }

    IEnumerator FadeTrigger()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);
    }
}
