using UnityEditor;
using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

public class TitleStart : MonoBehaviour
{
    public Menu Menu;
    public static TitleStart Instance;
    [SerializeField] private AudioClip sampleMusic;
    [SerializeField] private AudioClip sampleSfx;
    private GameObject spawnedMenu;
    private MMSoundManager soundManager;
    private float transitionSpeed = .2f;
    private static GameObject menuPrefab;
    private static int menuIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        menuPrefab = Menu.orderedPrefabs[menuIndex];
        spawnedMenu = Instantiate(menuPrefab) as GameObject;
        spawnedMenu.GetComponent<MenuFader>().FadeMenu(true);
    }

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<MMSoundManager>();
        Camera mainCamera = Camera.main;
        MMSoundManagerPlayOptions musicOptions = MMSoundManagerPlayOptions.Default;
        musicOptions.Loop = true;
        musicOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Master;
        musicOptions.Location = mainCamera.transform.position;
        soundManager.PlaySound(sampleMusic, musicOptions);

        MMSoundManagerPlayOptions sfxOptions = MMSoundManagerPlayOptions.Default;
        musicOptions.Loop = false;
        musicOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
        musicOptions.Location = mainCamera.transform.position;
        soundManager.PlaySound(sampleMusic, musicOptions);
    }

    public void UpdateMenu(bool menuProgression)
    {
        if (menuProgression)
        {
            menuIndex++;
        }

        else
        {
            menuIndex--;
        }

        UpdateMenuPrefab();
        StartCoroutine(OpenMenu());
    }

    private void UpdateMenuPrefab()
    {
        if (menuIndex <= 0)
        {
            menuIndex = 0;
        }

        else if (menuIndex >= Menu.orderedPrefabs.Length)
        {
            menuIndex = Menu.orderedPrefabs.Length;
        }

        menuPrefab = Menu.orderedPrefabs[menuIndex];
    }

    IEnumerator OpenMenu()
    {
        spawnedMenu.GetComponent<MenuFader>().FadeMenu(false);
        yield return new WaitForSeconds(spawnedMenu.GetComponent<MenuFader>().fadeDuration);
        Destroy(spawnedMenu);
        UpdateMenuPrefab();
        yield return new WaitForSeconds(transitionSpeed);
        spawnedMenu = Instantiate(menuPrefab);
        spawnedMenu.GetComponent<MenuFader>().FadeMenu(true);
    }
}
