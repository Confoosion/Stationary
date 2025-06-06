using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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

    public void CreateUIFaces(Box box)
    {
        // Iterate through each box face
        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            // If this face doesn't exist, skip
            if (!box.data.boxDetails.ContainsKey(face))
            {
                continue;
            }

            Transform UI_Face = transform.Find(face.ToString());

            var faceDetails = box.data.boxDetails[face];

            // Iterate through each detailType
            foreach (BoxDetailType detailType in System.Enum.GetValues(typeof(BoxDetailType)))
            {
                if (faceDetails.TryGetValue(detailType, out string detail))
                {
                    box.data.detailPositions.Add(detail, DisplayDetail(UI_Face, detailType, detail));
                }
            }
        }
        box.data.generatedUI = true;
    }

    public void UseCreatedUIFaces(Box box)
    {
        foreach (BoxFaces face in System.Enum.GetValues(typeof(BoxFaces)))
        {
            // If this face doesn't exist, skip
            if (!box.data.boxDetails.ContainsKey(face))
            {
                continue;
            }

            Transform UI_Face = transform.Find(face.ToString());
            var faceDetails = box.data.boxDetails[face];

            // Iterate through each detailType
            foreach (BoxDetailType detailType in System.Enum.GetValues(typeof(BoxDetailType)))
            {
                if (faceDetails.TryGetValue(detailType, out string detail))
                {
                    if (box.data.detailPositions.TryGetValue(detail, out Vector2 detailLocation))
                    {
                        // Make some way to instantiate any detail (Needs the GameObject)
                    }
                }
            }
        }
    }

    private Vector2 DisplayDetail(Transform faceParent, BoxDetailType detailType, string detail)
    {
        if (detailType == BoxDetailType.TapePattern)
        {
            ResourceManager.Singleton.TapeDesigns_BD.TryGetValue(detail.ToString(), out GameObject detailObject);
            Instantiate(detailObject, faceParent);

            return (new Vector2(0f, 0f));
        }

        switch (detailType)
        {
            case BoxDetailType.Color:
                {
                    ResourceManager.Singleton.Colors_BD.TryGetValue(detail.ToString(), out GameObject detailObject);
                    RectTransform rt = (RectTransform)detailObject.transform;
                    Vector2 detailLocation = new Vector2(Random.Range(-200f + rt.rect.width, 200f - rt.rect.width),
                                                         Random.Range(-200f + rt.rect.height, 200f - rt.rect.height));
                    Instantiate(detailObject, detailLocation, Quaternion.identity, faceParent);
                    return (detailLocation);
                }
            case BoxDetailType.Shape:
                {
                    ResourceManager.Singleton.Shapes_BD.TryGetValue(detail.ToString(), out GameObject detailObject);
                    RectTransform rt = (RectTransform)detailObject.transform;
                    Vector2 detailLocation = new Vector2(Random.Range(-200f + rt.rect.width, 200f - rt.rect.width),
                                                         Random.Range(-200f + rt.rect.height, 200f - rt.rect.height));
                    Instantiate(detailObject, detailLocation, Quaternion.identity, faceParent);
                    return (detailLocation);
                }
        }
        return (new Vector2(0f, 0f));
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
}
