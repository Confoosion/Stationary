using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    public BoxData data;

    void Start()
    {
        if (GameManager.Singleton.betterDurability)
        {
            data.durabilityDecreaseRate = 4f;
        }
    }
}
