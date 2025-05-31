using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InspectPackage : MonoBehaviour
{
    public GameObject inspectCanvas;
    public Box inspectedBox;
    public BoxFaces inspectedFace;
    private bool wantRotateShown = true;
    private bool isInspecting = false;
    [SerializeField] private List<GameObject> packagesOnTable = new List<GameObject>();

    void Awake()
    {
        PlayerKeybinds.Singleton.rotateAction.action.started += RotateKeybind;
        PlayerKeybinds.Singleton.inspectAction.action.started += InspectKeybind;
    }

    void OnDisable()
    {
        PlayerKeybinds.Singleton.rotateAction.action.started -= RotateKeybind;
        PlayerKeybinds.Singleton.inspectAction.action.started -= InspectKeybind;
    }

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

    private void InspectKeybind(InputAction.CallbackContext ctx)
    {
        if (!isInspecting)
        {
            Inspect();
        }
        else
        {
            HideInspect();
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
            isInspecting = true;
            Debug.Log("Inspecting " + packagesOnTable[0]);
        }
    }

    public void HideInspect()
    {
        inspectCanvas.SetActive(false);
        isInspecting = false;
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
    }

    private void RotateKeybind(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx);
        if (isInspecting)
        {
            Vector2 rotation = ctx.ReadValue<Vector2>();

            if (rotation.x == 0 && rotation.y > 0)
            {
                RotateUp();
            }
            else if (rotation.x == 0 && rotation.y < 0)
            {
                RotateDown();
            }
            else if (rotation.x > 0 && rotation.y == 0)
            {
                RotateRight();
            }
            else if (rotation.x < 0 && rotation.y == 0)
            {
                RotateLeft();
            }
        }
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
        UIManager.Singleton.rotateLeftButton.gameObject.SetActive(wantRotateShown);
        UIManager.Singleton.rotateRightButton.gameObject.SetActive(wantRotateShown);

        if (!wantRotateShown)
        {
            if (inspectedFace == BoxFaces.Top)
            {
                UIManager.Singleton.rotateUpButton.gameObject.SetActive(wantRotateShown);
            }
            else
            {
                UIManager.Singleton.rotateDownButton.gameObject.SetActive(wantRotateShown);
            }
            return;
        }

        UIManager.Singleton.rotateDownButton.gameObject.SetActive(wantRotateShown);
        UIManager.Singleton.rotateUpButton.gameObject.SetActive(wantRotateShown);
    }
}
