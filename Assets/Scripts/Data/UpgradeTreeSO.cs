using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTree", menuName = "Mousetrap/Data/Upgrade Tree")]
public class UpgradeTreeSO : ScriptableObject
{
    public UpgradeSO[] upgrades;
    public int currentUpgradeIndex = 0;
}
