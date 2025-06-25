using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Upgrade
{
    public UpgradeTier upgradeTier;
    public string upgradeCost;

    public List<UpgradeEffect> effects = new();
}
