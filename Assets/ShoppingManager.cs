using UnityEngine;

public class ShoppingManager : MonoBehaviour
{
    public void NextShift()
    {
        GameManager.Singleton.StartNewShift();
    }

    public void UpgradeConveyor()
    {
        if (PlayerStats.Singleton.GetMoney() > 0)
        {
            GameManager.Singleton.UpgradeConveyorLevel();
            Debug.Log("UPGRADED CONVEYOR!!!");
        }
    }
}
