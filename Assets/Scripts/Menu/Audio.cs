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
        isMasterOn = soundManager.settingsSo.Settings.MasterOn;
        isSfxOn = soundManager.settingsSo.Settings.SfxOn;

        if (isMasterOn)
        {
            masterText.color = new Color32(88, 255, 17, 255);
            masterText.text = "On";
            isMasterOn = false;
        }

        else
        {
            masterText.color = new Color32(244, 25, 0, 255);
            masterText.text = "Off";
            isMasterOn = true;
        }

        if (isSfxOn)
        {
            masterText.color = new Color32(88, 255, 17, 255);
            masterText.text = "On";
            isSfxOn = false;
        }

        else
        {
            masterText.color = new Color32(244, 25, 0, 255);
            masterText.text = "Off";
            isSfxOn = true;
        }
    }

    public void ToggleMaster()
    {
        if (isMasterOn)
        {
            masterText.color = new Color32(244, 25, 0, 255);
            masterText.text = "Off";
            soundManager.settingsSo.Settings.MasterOn = false;
            soundManager.SaveSettings();
            isMasterOn = false;
        }

        else
        {
            masterText.color = new Color32(88, 255, 17, 255);
            masterText.text = "On";
            soundManager.settingsSo.Settings.MasterOn = true;
            soundManager.SaveSettings();
            isMasterOn = true;
        }
    }

    public void ToggleSFX()
    {
        if (isSfxOn)
        {
            sfxText.color = new Color32(244, 25, 0, 255);
            sfxText.text = "Off";
            soundManager.settingsSo.Settings.SfxOn = false;
            soundManager.SaveSettings();
            isSfxOn = false;
        }

        else
        {
            sfxText.color = new Color32(88, 255, 17, 255);
            sfxText.text = "On";
            soundManager.settingsSo.Settings.SfxOn = true;
            soundManager.SaveSettings();
            isSfxOn = true;
        }
    }
}
