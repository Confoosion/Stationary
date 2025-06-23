using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Singleton { get; private set; }
    public List<Upgrade> lockedUpgrades = new List<Upgrade>();
    public List<Upgrade> unlockedUpgrades = new List<Upgrade>();

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;   
        }
    }

    public void UnlockUpgrade(Upgrade upgrade)
    {
        upgrade.ApplyUpgrade(GameManager.Singleton);
        lockedUpgrades.Remove(upgrade);
        unlockedUpgrades.Add(upgrade);
    }
}
