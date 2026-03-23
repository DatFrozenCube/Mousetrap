using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Mousetrap/Data")]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;
    public Sprite upgradeSprite;
    public int upgradeCost;
    public UpgradeType upgradeType;

    public enum UpgradeType
    {
        Speed,
        Health,
        CoinCapacity,
        PointsMultiplier
    }
}