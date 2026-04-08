using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTree", menuName = "Mousetrap/Data/Upgrade Tree")]
public class UpgradeTreeSO : ScriptableObject
{
    public UpgradeSO[] upgrades;
}
