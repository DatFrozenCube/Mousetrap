using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private float healthUpgradeAmount = 20f;
    [SerializeField] private float coinUpgradeMultiplier = 1.5f;
    [SerializeField] private float pointsUpgradeMultiplier = 2f;

    public static UpgradeController Instance;

    private void Start()
    {
        Instance = this;
    }

    public void ApplyUpgrade(UpgradeSO upgrade)
    {
        Health playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        switch (upgrade.upgradeType)
        {
            case UpgradeSO.UpgradeType.Health:
                float maxHealth = playerHealth.GetMaxHealth();
                playerHealth.SetMaxHealth(maxHealth + healthUpgradeAmount);
                MoneyController.Instance.SubtractMoney(upgrade.upgradeCost);
                break;

            case UpgradeSO.UpgradeType.CoinCapacity:
                MoneyController.Instance.MultiplyCapacity(2);
                MoneyController.Instance.SubtractMoney(upgrade.upgradeCost);
                break;
        }
    }
}
