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
    }

    public void NewShift()
    {
        requirementTheme = (BoxDetailType)(Random.Range(0, System.Enum.GetNames(typeof(BoxDetailType)).Length));
        SetNewRequirements(requirementTheme);
    }

    private void SetNewRequirements(BoxDetailType theme)
    {
        List<string> values = new List<string>();

        foreach (Cart cart in carts)
        {
            cart.ClearRequirements();
            string value = BoxManager.Singleton.GetRandomDetailValue(theme);

            while (values.Contains(value))
            {
                value = BoxManager.Singleton.GetRandomDetailValue(theme);
            }

            values.Add(value);
            cart.AddRequirement(theme, value);
            Debug.Log(cart + " " + theme + " " + value);
        }

        BoxManager.Singleton.RetrieveCartValues(requirementTheme, values);
    }

}
