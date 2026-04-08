using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data;

public class Shop : MonoBehaviour
{
    [SerializeField] private UpgradeTreeSO upgradeTree1;
    [SerializeField] private UpgradeTreeSO upgradeTree2;
    [SerializeField] private UpgradeTreeSO upgradeTree3;
    [SerializeField] private int upgrade1Index = 0;
    [SerializeField] private int upgrade2Index = 0;
    [SerializeField] private int upgrade3Index = 0;
    private UpgradeSO upgrade1;
    private UpgradeSO upgrade2;
    private UpgradeSO upgrade3;
    [SerializeField] private TMP_Text upgrade1Text;
    [SerializeField] private TMP_Text upgrade2Text;
    [SerializeField] private TMP_Text upgrade3Text;
    [SerializeField] private TMP_Text upgrade1Description;
    [SerializeField] private TMP_Text upgrade2Description;
    [SerializeField] private TMP_Text upgrade3Description;
    [SerializeField] private TMP_Text upgrade1Cost;
    [SerializeField] private TMP_Text upgrade2Cost;
    [SerializeField] private TMP_Text upgrade3Cost;
    [SerializeField] private Image upgrade1Icon;
    [SerializeField] private Image upgrade2Icon;
    [SerializeField] private Image upgrade3Icon;

    private Color goldYellow;

    private void Awake()
    {
        upgrade1 = upgradeTree1.upgrades[upgrade1Index];
        upgrade2 = upgradeTree2.upgrades[upgrade2Index];
        upgrade3 = upgradeTree3.upgrades[upgrade3Index];

        upgrade1Text.text = upgrade1.upgradeName;
        upgrade2Text.text = upgrade2.upgradeName;
        upgrade3Text.text = upgrade3.upgradeName;
        upgrade1Description.text = upgrade1.upgradeDescription;
        upgrade2Description.text = upgrade2.upgradeDescription;
        upgrade3Description.text = upgrade3.upgradeDescription;
        upgrade1Cost.text = upgrade1.upgradeCost.ToString();
        upgrade2Cost.text = upgrade2.upgradeCost.ToString();
        upgrade3Cost.text = upgrade3.upgradeCost.ToString();
        upgrade1Icon.sprite = upgrade1.upgradeSprite;
        upgrade2Icon.sprite = upgrade2.upgradeSprite;
        upgrade3Icon.sprite = upgrade3.upgradeSprite;

        goldYellow = new Color(1f, 0.9529412f, 0.5686275f);
    }

    private void Update()
    {
        if (upgrade1.upgradeCost > MoneyController.Instance.GetMoney())
        {
            upgrade1Cost.color = Color.darkRed;
        }
        else
        {
            upgrade1Cost.color = goldYellow;
        }

        if (upgrade2.upgradeCost > MoneyController.Instance.GetMoney())
        {
            upgrade2Cost.color = Color.darkRed;
        }
        else
        {
            upgrade2Cost.color = goldYellow;
        }

        if (upgrade3.upgradeCost > MoneyController.Instance.GetMoney())
        {
            upgrade3Cost.color = Color.darkRed;
        }
        else
        {
            upgrade3Cost.color = goldYellow;
        }
    }

    private void NextUpgrade()
    {

    }

    public void BuyUpgrade(UpgradeTreeSO upgradeTree)
    {
        if (MoneyController.Instance.GetMoney() >= upgradeTree.upgradeCost)
        {
            UpgradeController.Instance.ApplyUpgrade(upgrade);
            UpgradeController.Instance.NextUpgrade(upgradeTree);
        }
    }
}
