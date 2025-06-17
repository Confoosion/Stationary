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
        if (TryToBuyUpgrade(100 * GameManager.Singleton.GetConveyorLevel()))
        {
            GameManager.Singleton.UpgradeConveyorLevel();
            Debug.Log("UPGRADED CONVEYOR!!!");
            if (GameManager.Singleton.GetConveyorLevel() < 4)
            {
                ShoppingUI.Singleton.conveyorUpgrade_Cost.SetText("$" + (100 * GameManager.Singleton.GetConveyorLevel()).ToString());
            }
            else
            {
                ShoppingUI.Singleton.conveyorUpgrade_Cost.SetText("SOLD OUT!");
            }
        }
    }

    public void UpgradeBetterDurability()
    {
        if (TryToBuyUpgrade(150, true, ShoppingUI.Singleton.betterDurability_Cost))
        {
            GameManager.Singleton.betterDurability = true;
            Debug.Log("UPGRADED BETTER DURABILITY!!!");
        }
       
        // ShoppingUI.Singleton.betterDurability_Cost.SetText("SOLD OUT!");
    }

    public void BuyGloves()
    {
        if (TryToBuyUpgrade(300, true, ShoppingUI.Singleton.gloves_Cost))
        {
            GameManager.Singleton.boughtGloves = true;
            Debug.Log("BOUGHT GLOVES");
        }
    }

    private bool TryToBuyUpgrade(int cost, bool oneTimePurchase = false, TextMeshProUGUI costText = null)
    {
        if (PlayerStats.Singleton.GetMoney() >= cost)
        {
            PlayerStats.Singleton.AddMoney(-cost);
            ShoppingUI.Singleton.UpdateMoneyUI();

            if (oneTimePurchase)
            {
                costText.SetText("SOLD OUT!");
            }

            return (true);
        }
        else
        {
            return (false);
        }
    }
}
