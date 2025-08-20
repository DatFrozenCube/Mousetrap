using System.Xml.Linq;
using MoreMountains;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings Instance;
    [SerializeField] private GameObject masterIndicator;
    [SerializeField] private GameObject musicIndicator;
    [SerializeField] private GameObject sfxIndicator;
    private TMP_Text masterText;
    private TMP_Text musicText;
    private TMP_Text sfxText;
    private MMSoundManager soundManager;
    private bool isMasterOn;
    private bool isMusicOn;
    private bool isSfxOn;

    private void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        masterText = masterIndicator.GetComponent<TMP_Text>();
        musicText = musicIndicator.GetComponent<TMP_Text>();
        sfxText = sfxIndicator.GetComponent<TMP_Text>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<MMSoundManager>();
        isMasterOn = !soundManager.IsMuted(MMSoundManager.MMSoundManagerTracks.Master);
        isMusicOn = !soundManager.IsMuted(MMSoundManager.MMSoundManagerTracks.Music);
        isSfxOn = !soundManager.IsMuted(MMSoundManager.MMSoundManagerTracks.Sfx);
        CheckMaster();
        CheckMusic();
        CheckSfx();
    }

    private void CheckMaster()
    {
        if (!isMasterOn)
        {
            masterText.color = new Color32(244, 25, 0, 255);
            masterText.text = "Off";
        }

        else
        {
            masterText.color = new Color32(88, 255, 17, 255);
            masterText.text = "On";
        }
    }

    private void CheckMusic()
    {
        if (!isMusicOn)
        {
            musicText.color = new Color32(244, 25, 0, 255);
            musicText.text = "Off";
        }

        else
        {
            musicText.color = new Color32(88, 255, 17, 255);
            musicText.text = "On";
        }
    }

    private void CheckSfx()
    {
        if (!isSfxOn)
        {
            sfxText.color = new Color32(244, 25, 0, 255);
            sfxText.text = "Off";
        }

        else
        {
            sfxText.color = new Color32(88, 255, 17, 255);
            sfxText.text = "On";
        }
    }

    public void ToggleMaster()
    {
        if (isMasterOn)
        {
            soundManager.MuteMaster();
            soundManager.SaveSettings();
            isMasterOn = false;
            CheckMaster();
        }

        else
        {
            soundManager.UnmuteMaster();
            soundManager.SaveSettings();
            isMasterOn = true;
            CheckMaster();
        }
    }

    public void ToggleMusic()
    {
        if (isMusicOn)
        {
            soundManager.MuteMusic();
            soundManager.SaveSettings();
            isMusicOn = false;
            CheckMusic();
        }

        else
        {
            soundManager.UnmuteMusic();
            soundManager.SaveSettings();
            isMusicOn = true;
            CheckMusic();
        }
    }

    public void ToggleSFX()
    {
        if (isSfxOn)
        {
            soundManager.MuteSfx();
            soundManager.SaveSettings();
            isSfxOn = false;
            CheckSfx();
        }

        else
        {
            soundManager.UnmuteSfx();
            soundManager.SaveSettings();
            isSfxOn = true;
            CheckSfx();
        }
    }
}
