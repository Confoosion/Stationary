using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BoxData
{
    // Holds the details of each face
    public Dictionary<BoxFaces, Dictionary<BoxDetailType, string>> boxDetails;
    // Holds the positions of each detail
    public Dictionary<BoxFaces, Dictionary<GameObject, Vector2>> detailPositions;
    public bool generatedUI;
    public int durability;
    public float durabilityDecreaseRate;

    public BoxData()
    {
        boxDetails = new Dictionary<BoxFaces, Dictionary<BoxDetailType, string>>();
        detailPositions = new Dictionary<BoxFaces, Dictionary<GameObject, Vector2>>();
        generatedUI = false;
        durability = 100;
        durabilityDecreaseRate = 2f;
    }
}
