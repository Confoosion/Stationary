using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;
    public int upgradeCost;

    public abstract void ApplyUpgrade();
}
