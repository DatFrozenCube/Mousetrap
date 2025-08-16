using UnityEngine;
using System.Collections;
using Unity.Hierarchy;
using Lean.Gui;
using Unity.VisualScripting;

public class MenuFader : MonoBehaviour
{
    public float fadeDuration = 2f;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //true is fade in, false is fade out
    public void FadeMenu(bool fadeDirection)
    {
        if (fadeDirection)
        {
            StartCoroutine(FadeIn());
        }

        else
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float start = 0f;
        float end = 1f;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(start, end, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = end;
    }

    IEnumerator FadeOut()
    {
        float start = 1f;
        float end = 0f;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(start, end, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
