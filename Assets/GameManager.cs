using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    public int shift = 0;
    public int conveyorLevel = 1;

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
}
