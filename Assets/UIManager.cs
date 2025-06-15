using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }

    [Header("Inspect Buttons")]
    public Button rotateUpButton;
    public Button rotateDownButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;

    [Header("Environment UI")]
    public TextMeshProUGUI shiftText;
    public TextMeshProUGUI shiftUI;
    public TextMeshProUGUI shiftTimer;
    public TextMeshProUGUI moneyUI;

    [Header("Requirement Paper UI")]
    public GameObject paperUI;
    public TextMeshProUGUI cartText;
    public TextMeshProUGUI cartRequirementsText;

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

    public void UpdateTimerUI(int minutes, int seconds)
    {
        shiftTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // if (minutes > 0)
        // {
        //     shiftTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // }
        // else
        // {
        //     shiftTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // }
    }

    public void ShowPaperUI(Cart cart)
    {
        paperUI.SetActive(true);

        char[] chars = cartText.text.ToCharArray();
        if (cart.cartID != chars[chars.Length - 1])     // If the cartID is different, change the paper UI
        {
            UpdatePaperUI(cart.cartID, cart.cartRequirements);
            return;
        }
    }

    public void HidePaperUI()
    {
        paperUI.SetActive(false);
    }

    public void UpdatePaperUI(char cart_text, List<CartRequirements> requirements)
    {
        cartText.SetText("Cart " + cart_text);

        cartRequirementsText.SetText("");
        foreach (CartRequirements requirement in requirements)
        {
            cartRequirementsText.SetText(cartRequirementsText.text + "- " + requirement.detailType.ToString() + ": " + requirement.value + "\n");
        }

        // cartRequirementsText.SetText(requirement);
    }
}
