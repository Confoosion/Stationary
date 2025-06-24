using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Effects/Durability")]
public class DurabilityEffect : UpgradeEffect
{
    public float decayRate;

    public override void ApplyEffect(UpgradeManager upgradeManager)
    {
        upgradeManager.boxDecayRate = decayRate;
    }
}
