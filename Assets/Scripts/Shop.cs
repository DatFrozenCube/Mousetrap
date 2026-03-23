using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private UpgradeSO upgrade1;
    [SerializeField] private UpgradeSO upgrade2;
    [SerializeField] private UpgradeSO upgrade3;
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

    private void Awake()
    {
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
}
