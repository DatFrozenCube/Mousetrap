using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private void Start()
    {
        Music.Instance.PlayGameMusic();
    }
}
