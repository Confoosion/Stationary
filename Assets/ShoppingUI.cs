using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingUI : MonoBehaviour
{
    public TextMeshProUGUI moneyUI;

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
