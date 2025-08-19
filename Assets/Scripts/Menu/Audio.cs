using System.Xml.Linq;
using MoreMountains;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;
    [SerializeField] private GameObject masterIndicator;
    [SerializeField] private GameObject sfxIndicator;
    private TMP_Text masterText;
    private TMP_Text sfxText;
    private MMSoundManager soundManager;
    private bool isMasterOn;
    private bool isSfxOn;

    private void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        masterText = masterIndicator.GetComponent<TMP_Text>();
        sfxText = sfxIndicator.GetComponent<TMP_Text>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<MMSoundManager>();
        isMasterOn = !soundManager.IsMuted(MMSoundManager.MMSoundManagerTracks.Master);
        isSfxOn = !soundManager.IsMuted(MMSoundManager.MMSoundManagerTracks.Sfx);
        CheckMaster();
        CheckSfx();
    }

    public void CheckMaster()
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

    public void CheckSfx()
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
