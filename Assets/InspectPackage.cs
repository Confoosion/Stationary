using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InspectPackage : MonoBehaviour
{
    public GameObject inspectCanvas;
    public Transform BoxUI;
    public Box inspectedBox;
    public BoxFaces inspectedFace;
    private BoxFaces previousInspectedFace;
    private bool wantRotateShown = true;
    private bool isInspecting = false;
    private Coroutine durabilityCountdown;
    private float boxDurability = 100f;
    private float boxDurabilityDecreaseRate;

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

            if (!inspectedBox.data.generatedUI)
            {   // Create UI face details if not already generated
                BoxDetailUI.Singleton.CreateUIFaces(inspectedBox);
            }
            else
            {   // Use already generated UI face details
                BoxDetailUI.Singleton.CreateUIFaces(inspectedBox, true);
            }

            BoxDetailUI.Singleton.HideALLFaces();
            BoxDetailUI.Singleton.ShowFace(inspectedFace, BoxFaces.Back);

            // Start decreasing durability of the box
            boxDurability = inspectedBox.data.durability;
            boxDurabilityDecreaseRate = inspectedBox.data.durabilityDecreaseRate;
            durabilityCountdown = StartCoroutine(ReduceBoxDurabilty()); // Start durability decrease coroutine
            Debug.Log("Inspecting " + packagesOnTable[0]);
        }
    }

    public void HideInspect()
    {
        isInspecting = false;
        if (durabilityCountdown != null)    // Stop durability from decreasing if it still is
        {
            StopCoroutine(durabilityCountdown);
            durabilityCountdown = null;
        }
        inspectCanvas.SetActive(false);
        BoxDetailUI.Singleton.RemoveALLDetails();
        Debug.Log("Stopped inspecting");
    }

    private IEnumerator ReduceBoxDurabilty()
    {
        while (boxDurability > 0f && boxDurabilityDecreaseRate > 0f)
        {
            boxDurability -= boxDurabilityDecreaseRate * Time.deltaTime;
            inspectedBox.data.durability = (int)boxDurability;
            UIManager.Singleton.UpdateDurabilityUI((int)boxDurability);

            if (boxDurability <= 0)
            {
                // durabilityCountdown = null;
                HideInspect();
                Destroy(inspectedBox.gameObject);
                yield break;
            }

            yield return null;
        }
    }

    private void UpdateBoxFace()
    {
        BoxDetailUI.Singleton.ShowFace(inspectedFace, previousInspectedFace);
    }

    private void RotateKeybind(InputAction.CallbackContext ctx)
    {
        // Debug.Log(ctx);
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
        if (inspectedFace == BoxFaces.Top)
        {
            return;
        }
        
        if (inspectedFace == BoxFaces.Bottom)
        {
            SetNewFace(BoxFaces.Front);
            wantRotateShown = !wantRotateShown;
        }
        else
        {
            SetNewFace(BoxFaces.Top);
            wantRotateShown = !wantRotateShown;
        }
        SetRotateUIVisibility();
    }

    public void RotateDown()
    {
        if (inspectedFace == BoxFaces.Bottom)
        {
            return;
        }

        if (inspectedFace == BoxFaces.Top)
        {
            SetNewFace(BoxFaces.Front);
            wantRotateShown = !wantRotateShown;
        }
        else
        {
            SetNewFace(BoxFaces.Bottom);
            wantRotateShown = !wantRotateShown;
        }
        SetRotateUIVisibility();
    }

    public void RotateLeft()
    {
        switch (inspectedFace)
        {
            case BoxFaces.Front:
                SetNewFace(BoxFaces.Left);
                break;
            case BoxFaces.Right:
                SetNewFace(BoxFaces.Front);
                break;
            case BoxFaces.Back:
                SetNewFace(BoxFaces.Right);
                break;
            case BoxFaces.Left:
                SetNewFace(BoxFaces.Back);
                break;
        }
    }

    public void RotateRight()
    {
        switch (inspectedFace)
        {
            case BoxFaces.Front:
                SetNewFace(BoxFaces.Right);
                break;
            case BoxFaces.Right:
                SetNewFace(BoxFaces.Back);
                break;
            case BoxFaces.Back:
                SetNewFace(BoxFaces.Left);
                break;
            case BoxFaces.Left:
                SetNewFace(BoxFaces.Front);
                break;
        }
    }

    private void SetNewFace(BoxFaces face)
    {
        previousInspectedFace = inspectedFace;
        inspectedFace = face;
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
