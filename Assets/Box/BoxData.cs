using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BoxData
{
    // Holds the details of each face
    public Dictionary<BoxFaces, Dictionary<BoxDetailType, string>> boxDetails;
    // Holds the positions of each detail
    public Dictionary<string, Vector2> detailPositions;
    public bool generatedUI;

    public BoxData()
    {
        boxDetails = new Dictionary<BoxFaces, Dictionary<BoxDetailType, string>>();
        detailPositions = new Dictionary<string, Vector2>();
        generatedUI = false;
    }
}
