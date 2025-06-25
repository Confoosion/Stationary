using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Singleton { get; private set; }
    public List<UpgradeDefinition> lockedUpgrades = new List<UpgradeDefinition>();
    public List<UpgradeDefinition> unlockedUpgrades = new List<UpgradeDefinition>();

    [Header("Upgrade Effects")]
    public float boxDecayRate = 10f;
    public float conveyorSpeed = 5f;
    public bool visibleRequirements = false;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;   
        }
    }

    public void UnlockUpgrade(UpgradeDefinition upgrade)
    {
        upgrade.ApplyUpgrade(GameManager.Singleton);
        lockedUpgrades.Remove(upgrade);
        unlockedUpgrades.Add(upgrade);
    }
}
