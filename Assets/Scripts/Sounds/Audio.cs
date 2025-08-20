using MoreMountains.Tools;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private MMSoundManager soundManager;

    private void Start()
    {
        Instance = this;
    }

    public void ButtonClick()
    {
        soundManager.PlaySound(buttonClick, volume: 0.25f, mmSoundManagerTrack: MMSoundManager.MMSoundManagerTracks.Sfx, location: Camera.main.transform.position);
    }
}
