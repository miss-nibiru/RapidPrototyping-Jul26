using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 120f;
    [SerializeField] private bool countDown = true;
    
    [SerializeField] private float phoneSpawnTime;
    [SerializeField] private float phoneRingTime;
    [SerializeField] private float phoneExpireTime;
    
    [SerializeField] private float emailSpawnTime;
    [SerializeField] private float emailWarningTime;
    [SerializeField] private float emailExpireTime;
    
    
    //time bonus possibly so it can feel more satisfying
    [SerializeField] private float perfectTime;
   
    public float CurrentTime { get; private set; }
    private bool _isRunning;

    private void Update()
    {
        if (!_isRunning) return;

        float delta = Time.deltaTime;

        if (countDown)
        {
            CurrentTime -= delta;
            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
                _isRunning = false;
                UIManager.Instance.UpdateTimerUI(CurrentTime);
                GameManager.Instance.OnTimeExpired();
                return;
            }
        }
        else
        {
            CurrentTime += delta;
        }

        UIManager.Instance.UpdateTimerUI(CurrentTime);
    }

    public void StartTimer()
    {
        _isRunning = true;
        CurrentTime = countDown ? gameTime : 0f;
        UIManager.Instance.UpdateTimerUI(CurrentTime);
    }

    public void StopTimer()
    {
        _isRunning = false;
    }
}
