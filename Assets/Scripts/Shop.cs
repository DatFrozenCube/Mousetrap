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
        ResetShop();
        RefreshUpgradeUI();
        goldYellow = new Color(1f, 0.9529412f, 0.5686275f);
    }

    private void ResetShop()
    {
        upgradeTree1.currentUpgradeIndex = 0;
        upgradeTree2.currentUpgradeIndex = 0;
        upgradeTree3.currentUpgradeIndex = 0;
    }

    private void RefreshUpgradeUI()
    {
        upgrade1 = upgradeTree1.upgrades[upgradeTree1.currentUpgradeIndex];
        upgrade2 = upgradeTree2.upgrades[upgradeTree2.currentUpgradeIndex];
        upgrade3 = upgradeTree3.upgrades[upgradeTree3.currentUpgradeIndex];

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

    public void BuyUpgrade(UpgradeTreeSO upgradeTree)
    {
        UpgradeSO currentUpgrade = upgradeTree.upgrades[upgradeTree.currentUpgradeIndex];
        if (MoneyController.Instance.GetMoney() >= currentUpgrade.upgradeCost)
        {
            if (upgradeTree.currentUpgradeIndex < upgradeTree.upgrades.Length)
            {
                UpgradeController.Instance.ApplyUpgrade(currentUpgrade);
                upgradeTree.currentUpgradeIndex += 1;
            }

            else
            {
                MoneyController.Instance.WarnPlayer("Max upgrade reached!", 2f, 0.5f);
            }
        }

        RefreshUpgradeUI();
    }
}
