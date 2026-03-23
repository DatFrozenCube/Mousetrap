using UnityEngine;
using EasyTextEffects;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    private int money = 0;

    public static MoneyController Instance;

    private void Start()
    {
        Instance = this;
        moneyText.text = "0";
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
        moneyText.gameObject.GetComponent<TextEffect>().Refresh();
    }

}
