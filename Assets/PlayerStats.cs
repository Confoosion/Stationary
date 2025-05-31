using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Singleton { get; private set; }

    private int money;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        SetMoney(20);
    }

    public void SetMoney(int value)
    {
        money = value;
        UIManager.Singleton.UpdateMoneyUI(money);
    }

    public void AddMoney(int value)
    {
        money += value;
        UIManager.Singleton.UpdateMoneyUI(money);
    }

    public int GetMoney()
    {
        return (money);
    }

}
