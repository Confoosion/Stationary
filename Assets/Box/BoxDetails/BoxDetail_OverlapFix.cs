using UnityEngine;

public class BoxDetail_OverlapFix : MonoBehaviour
{
    [SerializeField]
    private bool isOverlapping = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        isOverlapping = true;
    }

    void Awake()
    {
        if (isOverlapping)
        {
            Debug.Log(this.gameObject + " IS OVERLAPPING!");
        }
    }
}
