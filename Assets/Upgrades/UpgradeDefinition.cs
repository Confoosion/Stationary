using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Definition")]
public class UpgradeDefinition : ScriptableObject
{
    public string theUpgradeName;
    public string theUpgradeDescription;
    public int theUpgradeCost;

    public UpgradeTier currentTier;
    public List<Upgrade> upgradeTiers = new();

    public void ApplyUpgrade(GameManager gameManager)
    {
        gameManager.appliedUpgrades.Add(this);
    }
}
