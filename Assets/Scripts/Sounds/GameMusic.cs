using Unity.VisualScripting;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] private GameObject soundManagerPrefab;

    private void Start()
    {
        GameObject musicManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (musicManager != null)
        {
            Music.Instance.PlayGameMusic();
        }

        else
        {
            Instantiate(soundManagerPrefab);
            Music.Instance.PlayGameMusic();
        }
    }
}
