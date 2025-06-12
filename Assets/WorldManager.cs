using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Singleton { get; private set; }
    public List<Cart> carts = new List<Cart>();
    public BoxDetailType requirementTheme;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UIManager.Singleton.UpdateMoneyUI(PlayerStats.Singleton.GetMoney());
        NewShift();
    }

    public void NewShift()
    {
        UIManager.Singleton.UpdateShiftUI(GameManager.Singleton.GetShift());
        requirementTheme = (BoxDetailType)(Random.Range(0, System.Enum.GetNames(typeof(BoxDetailType)).Length));
        Debug.Log(requirementTheme);
        SetNewRequirements(requirementTheme);
    }

    public void EndShift()
    {
        GameManager.Singleton.GoToShopScene();
    }

    // Should prob make this a coroutine cuz it can break if while loop is still going and then gets called again :/
    private void SetNewRequirements(BoxDetailType theme)
    {
        List<string> values = new List<string>();
        char currentID = 'A';

        foreach (Cart cart in carts)
        {
            cart.ClearRequirements();
            string value = BoxManager.Singleton.GetRandomDetailValue(theme, true);

            while (values.Contains(value))
            {
                value = BoxManager.Singleton.GetRandomDetailValue(theme, true);
            }

            values.Add(value);
            cart.AddRequirement(theme, value);
            Debug.Log(cart + " " + theme + " " + value);

            // Set cartID (in order alphabetically [A-Z])
            cart.cartID = currentID;
            currentID++;
        }

        BoxManager.Singleton.RetrieveCartValues(requirementTheme, values);
    }

    private void SetRandomCartID(Cart cart)
    {
        cart.cartID = (char)Random.Range(65, 91);   // Randomly pick from alphabet (A-Z)

        foreach (Cart _cart in carts)
        {
            if (_cart == cart)
            {
                continue;
            }

            if (_cart.cartID == cart.cartID)
            {   // Found a duplicate, must be unique so recursive call
                SetRandomCartID(cart);
                break;
            }
        }
    }
}
