using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }
    private bool properPositioning = false;

    [SerializeField] private int shift = 0;

    [Header("UPGRADES")]
    public List<Upgrade> appliedUpgrades = new();
    [SerializeField] private int conveyorLevel = 1;
    public bool betterDurability = false;
    public bool boughtGloves = false;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartNewShift()
    {
        SceneManager.LoadScene("GameScene");
        shift += 1;
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public int GetShift()
    {
        return (shift);
    }

    public int GetConveyorLevel()
    {
        return (conveyorLevel);
    }

    public void UpgradeConveyorLevel()
    {
        conveyorLevel += 1;
    }

    public bool CheckProperPositioning()
    {
        return (properPositioning);
    }

    public void UpgradeProperPositioning()
    {
        properPositioning = true;
    }
}
