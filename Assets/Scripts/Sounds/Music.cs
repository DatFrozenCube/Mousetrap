using MoreMountains.Tools;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private MMSoundManager soundManager;

    private void Start()
    {
        soundManager.PlaySound(backgroundMusic, loop: true, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Music, volume: 0.5f, location: Camera.main.transform.position);
    }
}
