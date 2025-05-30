using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Button rotateUpButton;
    public Button rotateDownButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public TextMeshProUGUI shiftText;
    public TextMeshProUGUI shiftTimer;
    public TextMeshProUGUI money;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
