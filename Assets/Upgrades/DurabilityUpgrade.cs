using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Increase Box Durability")]
public class DurabilityUpgrade : Upgrade
{
    [Header("Float (Default decrease rate is 10)")]
    public float durabilityBoost;

    public override void ApplyUpgrade(GameManager gameManager)
    {
        gameManager.appliedUpgrades.Add(this);
    }
}
