using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingManager : MonoBehaviour
{
    public void NextShift()
    {
        GameManager.Singleton.StartNewShift();
    }

    public void UpgradeConveyor()
    {
        if (PlayerStats.Singleton.GetMoney() >= 100 * GameManager.Singleton.GetConveyorLevel())
        {
            GameManager.Singleton.UpgradeConveyorLevel();
            Debug.Log("UPGRADED CONVEYOR!!!");
            gameObject.GetComponent<ShoppingUI>().conveyorUpgrade_Cost.SetText((100 * GameManager.Singleton.GetConveyorLevel()).ToString());
        }
        else
        {
            Debug.Log("TOO POOR!!!");
        }
    }
}
