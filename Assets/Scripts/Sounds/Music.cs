using MoreMountains.Tools;
using UnityEngine;

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
        soundManager.PlaySound(backgroundMusic, loop: true, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Music, volume: 0.5f, location: Camera.main.transform.position);
    }

    public void PlayGameMusic()
    {
        soundManager.PlaySound(gameMusic, loop: true, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Music, volume: 0.5f, location: Camera.main.transform.position);
    }
}
