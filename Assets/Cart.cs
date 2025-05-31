using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CartRequirements
{
    public BoxDetailType detailType;
    public string value;
}

public class Cart : MonoBehaviour
{
    public List<CartRequirements> cartRequirements = new List<CartRequirements>();

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (CheckRequirements(collider.GetComponent<Box>()))
        {
            Debug.Log("CORRECT!!!");
            PlayerStats.Singleton.AddMoney(20);
        }
        else
        {
            Debug.Log("WRONG!!!");
        }

        Destroy(collider.gameObject);
    }

    public void AddRequirement(BoxDetailType type, string val)
    {
        cartRequirements.Add(new CartRequirements { detailType = type, value = val} );
    }

    private bool CheckRequirements(Box box)
    {
        // Looking for each requirement
        foreach (CartRequirements requirement in cartRequirements)
        {
            bool matching = false;

            foreach (var boxFace in box.data.boxDetails)
            {
                var faceDetails = boxFace.Value;

                if (faceDetails.TryGetValue(requirement.detailType, out string detailValue))
                {
                    if (detailValue == requirement.value)
                    {
                        matching = true;
                        break;
                    }
                }
            }

            if (!matching)
            {
                return (false);
            }
        }

        return (true);
    }

    public void ClearRequirements()
    {
        cartRequirements.Clear();
    }
}
