using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Upgrade
{
    public UpgradeTier upgradeTier;
    public string upgradeName;
    public string upgradeDescription;
    public string upgradeCost;

    public List<UpgradeEffect> effects = new();
}
