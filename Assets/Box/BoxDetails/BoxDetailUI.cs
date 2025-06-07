using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class BoxDetailUI : MonoBehaviour
{
    public static BoxDetailUI Singleton { get; private set; }

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void CreateUIFaces(Box box, bool alreadyGenerated = false)
    {
        // Iterate through each box face
        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            // If this face doesn't exist, skip
            if (!box.data.boxDetails.ContainsKey(face))
            {
                continue;
            }

            // Get face Transform parent
            Transform UI_Face = transform.Find(face.ToString());

            if (!alreadyGenerated)
            {   // Generate new UI details
                var faceDetails = box.data.boxDetails[face];
                box.data.detailPositions[face] = new Dictionary<GameObject, Vector2>();

                // Iterate through each detailType
                foreach (BoxDetailType detailType in System.Enum.GetValues(typeof(BoxDetailType)))
                {
                    if (faceDetails.TryGetValue(detailType, out string detail))
                    {
                        GameObject obj = FindDetailObject(detailType, detail);
                        Vector2 objPos = FindDetailPosition(detailType, obj);

                        // box.data.detailPositions.Add(face, new Dictionary<GameObject, Vector2>() { { obj, objPos } });
                        box.data.detailPositions[face].Add(obj, objPos);

                        DisplayDetail(UI_Face, obj, objPos);
                    }
                }
            }
            else
            {   // Find and display details from the box's data
                var detailPosDict = box.data.detailPositions[face];
                foreach (KeyValuePair<GameObject, Vector2> pair in detailPosDict)
                {
                    DisplayDetail(UI_Face, pair.Key, pair.Value);
                }
            }
        }
        box.data.generatedUI = true;
    }

    // public void UseCreatedUIFaces(Box box)
    // {
    //     foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
    //     {
    //         // If this face doesn't exist, skip
    //         if (!box.data.boxDetails.ContainsKey(face))
    //         {
    //             continue;
    //         }

    //         Transform UI_Face = transform.Find(face.ToString());
    //         var faceDetails = box.data.boxDetails[face];

    //         // Iterate through each detailType
    //         foreach (BoxDetailType detailType in System.Enum.GetValues(typeof(BoxDetailType)))
    //         {
    //             if (faceDetails.TryGetValue(detailType, out string detail))
    //             {
    //                 if (box.data.detailPositions.TryGetValue(detail, out Vector2 detailLocation))
    //                 {
    //                     // Make some way to instantiate any detail (Needs the GameObject)
    //                 }
    //             }
    //         }
    //     }
    // }

    private void DisplayDetail(Transform faceParent, GameObject detailObject, Vector2 detailLocation)
    {
        Instantiate(detailObject, detailLocation, Quaternion.identity, faceParent);
        // if (detailType == BoxDetailType.TapePattern)
        // {
        //     ResourceManager.Singleton.TapeDesigns_BD.TryGetValue(detail.ToString(), out GameObject detailObject);
        //     Instantiate(detailObject, faceParent);

        //     return (new Vector2(0f, 0f));
        // }

        // switch (detailType)
        // {
        //     case BoxDetailType.Color:
        //         {
        //             ResourceManager.Singleton.Colors_BD.TryGetValue(detail.ToString(), out GameObject detailObject);
        //             RectTransform rt = (RectTransform)detailObject.transform;
        //             Vector2 detailLocation = new Vector2(Random.Range(-200f + rt.rect.width, 200f - rt.rect.width),
        //                                                  Random.Range(-200f + rt.rect.height, 200f - rt.rect.height));
        //             Instantiate(detailObject, detailLocation, Quaternion.identity, faceParent);
        //             return (detailLocation);
        //         }
        //     case BoxDetailType.Shape:
        //         {
        //             ResourceManager.Singleton.Shapes_BD.TryGetValue(detail.ToString(), out GameObject detailObject);
        //             RectTransform rt = (RectTransform)detailObject.transform;
        //             Vector2 detailLocation = new Vector2(Random.Range(-200f + rt.rect.width, 200f - rt.rect.width),
        //                                                  Random.Range(-200f + rt.rect.height, 200f - rt.rect.height));
        //             Instantiate(detailObject, detailLocation, Quaternion.identity, faceParent);
        //             return (detailLocation);
        //         }
        // }
        // return (new Vector2(0f, 0f));
    }

    public void RemoveALLDetails()
    {
        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            Transform UI_Face = transform.Find(face.ToString());
            foreach (Transform detail in UI_Face)
            {
                GameObject.Destroy(detail.gameObject);
            }
        }
    }

    public void ShowFace(BoxFaces show, BoxFaces hide)
    {
        transform.Find(show.ToString()).gameObject.SetActive(true);
        transform.Find(hide.ToString()).gameObject.SetActive(false);
    }

    public void HideALLFaces()
    {
        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            transform.Find(face.ToString()).gameObject.SetActive(false);
        }
    }

    private GameObject FindDetailObject(BoxDetailType detailType, string detail)
    {
        // Unfortunately vibe coded after ~3 hours of debugging but the concept was natural T_T
        GameObject detailObject = null;

        switch (detailType)
        {
            case BoxDetailType.TapePattern:
                {
                    ResourceManager.Singleton.TapeDesigns_BD.TryGetValue(detail.ToString(), out detailObject);
                    break;
                }
            case BoxDetailType.Color:
                {
                    ResourceManager.Singleton.Colors_BD.TryGetValue(detail.ToString(), out detailObject);
                    break;
                }
            case BoxDetailType.Shape:
                {
                    ResourceManager.Singleton.Shapes_BD.TryGetValue(detail.ToString(), out detailObject);
                    break;
                }
            case BoxDetailType.BC:
            case BoxDetailType.ID:
                {
                    ResourceManager.Singleton.Tags_BD.TryGetValue(detailType.ToString(), out detailObject);
                    if (detailObject != null)
                    {
                        detailObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(detail.ToString());   
                    }
                    break;
                }
        }
        return (detailObject);
    }

    private Vector2 FindDetailPosition(BoxDetailType detailType, GameObject detailObject)
    {
        if (detailType == BoxDetailType.TapePattern)
        {
            return (new Vector2(0f, 0f));
        }

        RectTransform rt = detailObject.GetComponent<RectTransform>();
        // rt.localPosition =  new Vector2(Random.Range(-200f + rt.rect.width, 200f - rt.rect.width),
        //                                      Random.Range(-200f + rt.rect.height, 200f - rt.rect.height));
        Vector2 detailLocation = new Vector2(Random.Range(-200f + rt.rect.width * 0.5f, 200f - rt.rect.width * 0.5f),
                                             Random.Range(-200f + rt.rect.height * 0.5f, 200f - rt.rect.height * 0.5f));
        Debug.Log(detailLocation);
        return (detailLocation);
    }
}
