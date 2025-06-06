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

        List<BoxDetailType> availableDetails = new(System.Enum.GetValues(typeof(BoxDetailType)) as BoxDetailType[]);

        spawnedBox.GetComponent<Box>().data = GenerateBoxFaces(availableDetails);

        // spawnedBox.GetComponent<Box>().data = GenerateRandomBoxData();
    }

    private BoxData GenerateBoxFaces(List<BoxDetailType> available)
    {
        BoxData data = new BoxData();

        // Get the tape design first since that takes priority
        data = AssignTapeDesign(data);
        available.Remove(BoxDetailType.TapePattern);

        // Get the rest of the details that don't have any specific rules
        data = GenerateRandomBoxData(data, available);
        return (data);
    }

    private BoxData AssignTapeDesign(BoxData data)
    {
        // Get a face of the box
        // if the properPositioning upgrade is enabled then it will always be the top face
        BoxFaces tapeFace = BoxFaces.Top;
        if (!GameManager.Singleton.CheckProperPositioning())
        {
            tapeFace = ((BoxFaces)Random.Range(0, System.Enum.GetNames(typeof(BoxFaces)).Length));
        }
        data.boxDetails[tapeFace] = new Dictionary<BoxDetailType, string>();

        // This face now has the tape
        data.boxDetails[tapeFace][BoxDetailType.TapePattern] = GetRandomDetailValue(BoxDetailType.TapePattern);
        return (data);
    }

    private BoxData GenerateRandomBoxData(BoxData data, List<BoxDetailType> available)
    {
        // Iterate through each face to assign details to the faces
        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            // Check to see if this face already exists so we can skip it
            if (data.boxDetails.ContainsKey(face))
            {
                continue;
            }

            // Initialize new Dictionary for the box face
            data.boxDetails[face] = new Dictionary<BoxDetailType, string>();

            // Could use :
            //             int detailCount = Random.Range(0, System.Enum.GetNames(typeof(BoxDetailType)).Length);
            // but I don't want all detail types to be on one face at the moment.
            int detailCount = Random.Range(1, 3);

            // Put the details on the face
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
                BoxDetailType.BC => Random.Range(1000, 9999).ToString(),
                BoxDetailType.ID => Random.Range(0, 100).ToString(),
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
