using UnityEngine;
using UnityEngine.UI;
using EasyTextEffects;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Animator moneyBagAnimator;
    private int money = 0;

    public static MoneyController Instance;

    private void Start()
    {
        Instance = this;
        moneyText.text = "0";

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
        money += amount;
        moneyText.text = money.ToString();
        moneyText.gameObject.GetComponent<TextEffect>().Refresh();
    }

}
