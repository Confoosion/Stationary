using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<Upgrade> lockedUpgrades = new List<Upgrade>();
    public List<Upgrade> unlockedUpgrades = new List<Upgrade>();

    public void UnlockUpgrade(Upgrade upgrade)
    {
        upgrade.ApplyUpgrade();
        lockedUpgrades.Remove(upgrade);
        unlockedUpgrades.Add(upgrade);
    }
}
