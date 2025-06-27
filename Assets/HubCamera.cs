using UnityEngine;

public class HubCamera : MonoBehaviour
{
    private float scrollSpeed;
    private float rightSide;
    private float leftSide;

    void Awake()
    {
        rightSide = Screen.width * 0.9f;
        leftSide = Screen.width - Screen.width * 0.9f;
    }

    void Update()
    {
        if (Input.mousePosition.x > rightSide)
        {
            scrollSpeed = (Input.mousePosition.x - rightSide) * 0.15f;
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
        else if (Input.mousePosition.x < leftSide)
        {
            scrollSpeed = (leftSide - Input.mousePosition.x) * 0.15f;
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
    }
}
