using UnityEngine;

public class BoxDetail_OverlapFix : MonoBehaviour
{
    public bool isOverlapping = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        isOverlapping = true;
    }
}
