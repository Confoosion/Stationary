using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }

    public Button rotateUpButton;
    public Button rotateDownButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public TextMeshProUGUI shiftText;
    public TextMeshProUGUI shiftUI;
    public TextMeshProUGUI shiftTimer;
    public TextMeshProUGUI moneyUI;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void UpdateMoneyUI(int money)
    {
        moneyUI.SetText("$" + money.ToString());
    }

    public void UpdateShiftUI(int shift)
    {
        shiftUI.SetText(shift.ToString());
    }
}
