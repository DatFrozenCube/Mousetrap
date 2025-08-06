using System.Collections;
using UnityEngine;

public class CrossfadeController : MonoBehaviour
{
    public static CrossfadeController Instance;
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 2f;

    private void Awake()
    {
        Instance = this;
    }

    public void Fade()
    {
        StartCoroutine(NextLevel());
    }
    
    IEnumerator NextLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        LevelController.LevelNumber++;
        LevelController.DestroyLevel();
        LevelController.LevelActions.Invoke();
        transition.SetTrigger("End");
    }
}
