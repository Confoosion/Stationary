using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
    public static BoxManager Singleton { get; private set; }
    public GameObject boxPrefab;
    public Transform boxSpawnPoint;
    public Transform boxParent;

    public BoxDetailType cartTheme;
    public List<string> cartValues = new List<string>();

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void SpawnBox()
    {
        GameObject spawnedBox = Instantiate(boxPrefab, boxSpawnPoint.position, Quaternion.identity, boxParent);
        spawnedBox.GetComponent<Box>().data = GenerateRandomBoxData();
    }

    private BoxData GenerateRandomBoxData()
    {
        BoxData data = new BoxData();

        List<BoxDetailType> available = new(System.Enum.GetValues(typeof(BoxDetailType)) as BoxDetailType[]);

        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            data.boxDetails[face] = new Dictionary<BoxDetailType, string>();

            // Could use :
            //             int detailCount = Random.Range(0, System.Enum.GetNames(typeof(BoxDetailType)).Length);
            // but I don't want all detail types to be on one face at the moment.
            int detailCount = Random.Range(1, 3);

            for (int i = 0; i < detailCount && available.Count > 0; i++)
            {
                var index = Random.Range(0, available.Count);
                var type = available[index];
                available.RemoveAt(index);

                string value = GetRandomDetailValue(type);
                data.boxDetails[face][type] = value;
            }

            if (available.Count == 0)
            {
                break;
            }
        }

        return data;
    }

    public string GetRandomDetailValue(BoxDetailType type, bool valueForCart = false)
    {
        if (type != cartTheme || valueForCart)
        {
            return type switch
            {
                BoxDetailType.Barcode => Random.Range(1000, 9999).ToString(),
                BoxDetailType.BoxID => Random.Range(0, 100).ToString(),
                BoxDetailType.Color => ((BoxColor)Random.Range(0, System.Enum.GetNames(typeof(BoxColor)).Length)).ToString(),
                BoxDetailType.Shape => ((BoxShape)Random.Range(0, System.Enum.GetNames(typeof(BoxShape)).Length)).ToString(),
                BoxDetailType.TapePattern => ((BoxTapePattern)Random.Range(0, System.Enum.GetNames(typeof(BoxTapePattern)).Length)).ToString(),
                _ => "Unknown"
            };
        }
        return (cartValues[Random.Range(0, cartValues.Count)]);
    }

    public void RetrieveCartValues(BoxDetailType theme, List<string> values)
    {
        cartTheme = theme;
        cartValues = values;
    }
}
