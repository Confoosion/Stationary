using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Effects/Conveyor")]
public class ConveyorEffect : UpgradeEffect
{
    public float speed;
    public override void ApplyEffect(UpgradeManager upgradeManager)
    {
        upgradeManager.conveyorSpeed = speed;
    }
}
