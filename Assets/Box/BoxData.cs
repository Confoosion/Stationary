using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BoxData
{
    public Dictionary<BoxFaces, Dictionary<BoxDetailType, string>> boxDetails;

    public BoxData()
    {
        boxDetails = new Dictionary<BoxFaces, Dictionary<BoxDetailType, string>>();
    }
}
