using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Singleton { get; private set; }

    [SerializeField]
    private float countdownTimer;
    [SerializeField]
    private bool pausedTime = false;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTimer > 0f && !pausedTime)
        {
            countdownTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(countdownTimer / 60);
            int seconds = Mathf.FloorToInt(countdownTimer % 60);
            if (countdownTimer < 0f)
            {
                GameManager.Singleton.GoToShopScene();
                return;
            }
            UIManager.Singleton.UpdateTimerUI(minutes, seconds);
            
        }

    }

    public void SetTimer(float time)
    {
        countdownTimer = time;
        int minutes = Mathf.FloorToInt(countdownTimer / 60);
        int seconds = Mathf.FloorToInt(countdownTimer % 60);
        UIManager.Singleton.UpdateTimerUI(minutes, seconds);
    }

    public void PauseTimer(bool wantPaused = true)
    {
        pausedTime = wantPaused;
    }

    public float GetTimer()
    {
        return (countdownTimer);
    }
}
