using System.Collections;
using UnityEngine;

public class CrossfadeController : MonoBehaviour
{
    public static CrossfadeController Instance;
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 2f;
    private Mouse player;

    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mouse>();
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
        //player.ResetPlayerPosition();
        LevelController.LevelActions.Invoke();
        transition.SetTrigger("End");
    }
}
