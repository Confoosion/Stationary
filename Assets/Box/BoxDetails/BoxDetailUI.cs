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

    // This took 6+ hours to work LOL (the solution wasn't even this code it was the damned files fmscl)
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

                // Iterate through each detailType that exists
                foreach (KeyValuePair<BoxDetailType, string> pair in faceDetails)
                {
                    // Debug.Log("Box Detail Type: " + pair.Key + "\tBox Detail Value: " + pair.Value);
                    GameObject obj = FindDetailObject(pair.Key, pair.Value);
                    Vector2 objPos = FindDetailPosition(pair.Key, obj, UI_Face);

                    // box.data.detailPositions.Add(face, new Dictionary<GameObject, Vector2>() { { obj, objPos } });
                    box.data.detailPositions[face].Add(obj, objPos);

                    DisplayDetail(UI_Face, obj, objPos);
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

    private void DisplayDetail(Transform faceParent, GameObject detailObject, Vector2 detailLocation)
    {
        Instantiate(detailObject, detailLocation, Quaternion.identity, faceParent);
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

        // Debug.Log("LOOKING FOR: " + detailType);
        switch (detailType)
        {
            case BoxDetailType.TapePattern:
                {
                    ResourceManager.Singleton.TapeDesigns_BD.TryGetValue(detail.ToString(), out detailObject);
                    // Debug.Log("FOUND OBJECT: " + detailObject);
                    break;
                }
            case BoxDetailType.Color:
                {
                    ResourceManager.Singleton.Colors_BD.TryGetValue(detail.ToString(), out detailObject);
                    // Debug.Log("FOUND OBJECT: " + detailObject);
                    break;
                }
            case BoxDetailType.Shape:
                {
                    ResourceManager.Singleton.Shapes_BD.TryGetValue(detail.ToString(), out detailObject);
                    // Debug.Log("FOUND OBJECT: " + detailObject);
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
                    // Debug.Log("FOUND OBJECT: " + detailObject);
                    break;
                }
        }
        return (detailObject);
    }

    private Vector2 FindDetailPosition(BoxDetailType detailType, GameObject detailObject, Transform detailFace)
    {
        if (detailType == BoxDetailType.TapePattern)
        {
            return (detailFace.position);
        }

        // Debug.Log(detailObject);
        RectTransform rt = detailObject.GetComponent<RectTransform>();
        // rt.localPosition =  new Vector2(Random.Range(-200f + rt.rect.width, 200f - rt.rect.width),
        //                                      Random.Range(-200f + rt.rect.height, 200f - rt.rect.height));
        Vector2 detailLocation = new Vector2(Random.Range(detailFace.position.x - 200f + rt.rect.width * 0.5f, detailFace.position.x + 200f - rt.rect.width * 0.5f),
                                             Random.Range(detailFace.position.y - 200f + rt.rect.height * 0.5f, detailFace.position.y + 200f - rt.rect.height * 0.5f));
        // Debug.Log(detailLocation);
        return (detailLocation);
    }
}
