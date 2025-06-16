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
            PlayerStats.Singleton.AddMoney(100 * GameManager.Singleton.GetConveyorLevel());
            GameManager.Singleton.UpgradeConveyorLevel();
            Debug.Log("UPGRADED CONVEYOR!!!");
            gameObject.GetComponent<ShoppingUI>().conveyorUpgrade_Cost.SetText("$" + (100 * GameManager.Singleton.GetConveyorLevel()).ToString());
        }
        else
        {
            Debug.Log("TOO POOR!!!");
        }
    }

    public void UpgradeBetterDurability()
    {
        if (PlayerStats.Singleton.GetMoney() >= 150)
        {
            PlayerStats.Singleton.AddMoney(-150);
            GameManager.Singleton.betterDurability = true;
            Debug.Log("UPGRADED BETTER DURABILITY!!!");
            gameObject.GetComponent<ShoppingUI>().betterDurability_Cost.SetText("SOLD OUT!");
        }
        else
        {
            Debug.Log("TOO POOR!!!");
        }
    }
}
