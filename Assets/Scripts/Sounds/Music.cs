using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public static Music Instance;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private MMSoundManager soundManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayBackgroundMusic();
        }

        else
        {
            PlayGameMusic();
        }
    }

    public void PlayBackgroundMusic()
    {
        soundManager.PlaySound(backgroundMusic, loop: true, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Music, volume: 0.5f, location: Camera.main.transform.position);
    }

    public void PlayGameMusic()
    {
        soundManager.PlaySound(gameMusic, loop: true, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Music, volume: 0.5f, location: Camera.main.transform.position);
    }
}
