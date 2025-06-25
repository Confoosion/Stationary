using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Effects/Visible Requirements")]
public class VisibleRequirementsEffect : UpgradeEffect
{
    public override void ApplyEffect(UpgradeManager upgradeManager)
    {
        upgradeManager.visibleRequirements = true;
    }
}
