using UnityEditor;
using UnityEngine;
using System.Collections;

public class TitleStart : MonoBehaviour
{
    public Menu Menu;
    public static TitleStart Instance;
    private GameObject spawnedMenu;
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
