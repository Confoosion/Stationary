using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoxDetailUI : MonoBehaviour
{
    public static BoxDetailUI Singleton { get; private set; }

    public GameObject top;
    public GameObject left;
    public GameObject front;
    public GameObject right;
    public GameObject back;
    public GameObject bottom;

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

            foreach (BoxDetailType detailType in System.Enum.GetValues(typeof(BoxDetailType)))
            {
                if (faceDetails.TryGetValue(detailType, out string detail))
                {
                    Debug.Log(detail);
                }
            }

        }
    }

    public void ShowFace(BoxFaces show, BoxFaces hide)
    {
        transform.Find(show.ToString()).gameObject.SetActive(true);
        transform.Find(hide.ToString()).gameObject.SetActive(false);
    }
}
