using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InspectPackage : MonoBehaviour
{
    public GameObject inspectCanvas;
    public Box inspectedBox;
    public BoxFaces inspectedFace;
    private bool wantRotateShown = true;
    [SerializeField] private List<GameObject> packagesOnTable = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!packagesOnTable.Contains(collider.gameObject))
        {
            packagesOnTable.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (packagesOnTable.Contains(collider.gameObject))
        {
            packagesOnTable.Remove(collider.gameObject);
        }
    }

    public void Inspect()
    {
        if (packagesOnTable.Count > 0 && !inspectCanvas.activeSelf)
        {
            inspectedBox = packagesOnTable[0].GetComponent<Box>();
            inspectedFace = BoxFaces.Front;
            wantRotateShown = true;
            SetRotateUIVisibility();

            inspectCanvas.SetActive(true);
            Debug.Log("Inspecting " + packagesOnTable[0]);
        }
    }

    public void HideInspect()
    {
        inspectCanvas.SetActive(false);
        Debug.Log("Stopped inspecting");
    }

    private void UpdateBoxFace()
    {
        if (inspectedBox.data.boxDetails.ContainsKey(inspectedFace))
        {
            var faceDetails = inspectedBox.data.boxDetails[inspectedFace];

            foreach (BoxDetailType detailType in System.Enum.GetValues(typeof(BoxDetailType)))
            {
                if (faceDetails.TryGetValue(detailType, out string detail))
                {
                    Debug.Log(detail);
                }
            }
        }
        // Debug.Log(faceDetails[0]);
        // Debug.Log(inspectedBox.data.boxDetails[inspectedFace]);
    }

    public void RotateUp()
    {
        if (inspectedFace == BoxFaces.Bottom)
        {
            inspectedFace = BoxFaces.Front;
            wantRotateShown = !wantRotateShown;
        }
        else
        {
            inspectedFace = BoxFaces.Top;
            wantRotateShown = !wantRotateShown;
        }
        SetRotateUIVisibility();
        Debug.Log("Now looking at " + inspectedFace);
        UpdateBoxFace();
    }

    public void RotateDown()
    {
        if (inspectedFace == BoxFaces.Top)
        {
            inspectedFace = BoxFaces.Front;
            wantRotateShown = !wantRotateShown;
        }
        else
        {
            inspectedFace = BoxFaces.Bottom;
            wantRotateShown = !wantRotateShown;
        }
        SetRotateUIVisibility();
        Debug.Log("Now looking at " + inspectedFace);
        UpdateBoxFace();
    }

    public void RotateLeft()
    {
        switch (inspectedFace)
        {
            case BoxFaces.Front:
                inspectedFace = BoxFaces.Left;
                break;
            case BoxFaces.Right:
                inspectedFace = BoxFaces.Front;
                break;
            case BoxFaces.Back:
                inspectedFace = BoxFaces.Right;
                break;
            case BoxFaces.Left:
                inspectedFace = BoxFaces.Back;
                break;
        }
        Debug.Log("Now looking at " + inspectedFace);
        UpdateBoxFace();
    }

    public void RotateRight()
    {
        switch (inspectedFace)
        {
            case BoxFaces.Front:
                inspectedFace = BoxFaces.Right;
                break;
            case BoxFaces.Right:
                inspectedFace = BoxFaces.Back;
                break;
            case BoxFaces.Back:
                inspectedFace = BoxFaces.Left;
                break;
            case BoxFaces.Left:
                inspectedFace = BoxFaces.Front;
                break;
        }
        Debug.Log("Now looking at " + inspectedFace);
        UpdateBoxFace();
    }

    public void SetRotateUIVisibility()
    {
        UIManager.Instance.rotateLeftButton.gameObject.SetActive(wantRotateShown);
        UIManager.Instance.rotateRightButton.gameObject.SetActive(wantRotateShown);

        if (!wantRotateShown)
        {
            if (inspectedFace == BoxFaces.Top)
            {
                UIManager.Instance.rotateUpButton.gameObject.SetActive(wantRotateShown);
            }
            else
            {
                UIManager.Instance.rotateDownButton.gameObject.SetActive(wantRotateShown);
            }
            return;
        }

        UIManager.Instance.rotateDownButton.gameObject.SetActive(wantRotateShown);
        UIManager.Instance.rotateUpButton.gameObject.SetActive(wantRotateShown);
    }
}
