using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingUI : MonoBehaviour
{
    public TextMeshProUGUI moneyUI;

    [Header("Upgrade Costs")]
    public TextMeshProUGUI conveyorUpgrade_Cost;
    public TextMeshProUGUI betterDurability_Cost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        moneyUI.SetText("$" + PlayerStats.Singleton.GetMoney().ToString());
    }


}
