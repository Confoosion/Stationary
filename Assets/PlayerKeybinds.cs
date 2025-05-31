using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeybinds : MonoBehaviour
{
    public static PlayerKeybinds Singleton { get; private set; }

    public InputActionReference rotateAction;
    public InputActionReference inspectAction;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }
}
