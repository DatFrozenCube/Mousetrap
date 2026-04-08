using UnityEngine;
using UnityEngine.UI;
using EasyTextEffects;
using TMPro;
using System.Collections;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Animator moneyBagAnimator;
    [SerializeField] private TMP_Text warningText;
    private int money = 0;
    private int capacity = 50;

    public static MoneyController Instance;

    private void Start()
    {
        Instance = this;
        moneyText.text = "0";
        warningText.gameObject.SetActive(false);

        PauseManager.pauseActions += moneyText.gameObject.GetComponent<TextEffect>().StopAllEffects;
        PauseManager.resumeActions += moneyText.gameObject.GetComponent<TextEffect>().StartOnStartEffects;
        PauseManager.pauseActions += moneyBagAnimator.StartPlayback;
        PauseManager.resumeActions += moneyBagAnimator.StopPlayback;
    }

    private void OnDestroy()
    {
        PauseManager.pauseActions -= moneyText.gameObject.GetComponent<TextEffect>().StopAllEffects;
        PauseManager.resumeActions -= moneyText.gameObject.GetComponent<TextEffect>().StartOnStartEffects;
        PauseManager.pauseActions -= moneyBagAnimator.StartPlayback;
        PauseManager.resumeActions -= moneyBagAnimator.StopPlayback;
    }

    public void AddMoney(int amount)
    {
        if (money + amount > capacity)
        {
            money = capacity;
        }

        else
        {
            money += amount;
        }

        moneyText.text = money.ToString();
        moneyText.gameObject.GetComponent<TextEffect>().Refresh();
    }

    public void SubtractMoney(int amount)
    {
        if (money - amount < 0)
        {
            money = 0;
        }

        else
        {
            money -= amount;
        }

        moneyText.text = money.ToString();
        moneyText.gameObject.GetComponent<TextEffect>().Refresh();
    }

    public void MultiplyCapacity(int amount)
    {
        capacity = Mathf.RoundToInt(capacity * amount);
    }

    public int GetCapacity()
    {
        return capacity;
    }

    public int GetMoney()
    {
        return money;
    }

    public void WarnPlayer(float duration, float fadeDuration)
    {
        StartCoroutine(FadeWarning(duration, fadeDuration));
    }

    public IEnumerator FadeWarning(float duration, float fadeDuration)
    {
        warningText.alpha = 0f;
        warningText.gameObject.SetActive(true);
        
        while (warningText.alpha < 1f)
        {
            warningText.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        while (warningText.alpha > 0f)
        {
            warningText.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        warningText.gameObject.SetActive(false);
    }
}
