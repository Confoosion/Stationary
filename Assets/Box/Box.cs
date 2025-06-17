using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    public BoxData data;

    void Start()
    {
        // Checking for Upgrades
        if (GameManager.Singleton.betterDurability)
        {
            data.durabilityDecreaseRate -= 4f;
        }
        if (GameManager.Singleton.boughtGloves)
        {
            data.durabilityDecreaseRate -= 6f;
        }
    }
}
