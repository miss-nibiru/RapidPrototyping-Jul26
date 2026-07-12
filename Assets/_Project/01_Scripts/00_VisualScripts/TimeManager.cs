using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        [Header("Game Time")]
        [SerializeField] public float gameTime = 120f;
        [SerializeField] private bool countDown = true;

        [Header("Phone Timing")]
        [SerializeField] private float phoneSpawnTime;

        [Header("Email Timing")]
        [SerializeField] private float emailBannerSpawnTime;

        [Header("Bonus/Penalty Amounts")]
        [SerializeField] private float smallTimeGainAmount = 3f;
        [SerializeField] private float smallTimePenaltyAmount = 5f;
        [SerializeField] private float largeTimeGainAmount = 8f;
        [SerializeField] private float largeTimePenaltyAmount = 10f;

        public float CurrentTime { get; private set; }

        // NEW: Tracks actual survival time
        private float _survivalTime;

        private bool _isRunning;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (!_isRunning) return;

            float delta = Time.deltaTime;

            // ALWAYS track survival time
            _survivalTime += delta;

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
            _survivalTime = 0f; // reset survival time

            CurrentTime = countDown ? gameTime : 0f;
            UIManager.Instance.UpdateTimerUI(CurrentTime);
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        // NEW: Correct elapsed/survival time
        public float GetSurvivalTime()
        {
            return _survivalTime;
        }

        public void AddTime(float amount)
        {
            CurrentTime += amount;
            UIManager.Instance.UpdateTimerUI(CurrentTime);
        }

        public void SubtractTime(float amount)
        {
            CurrentTime -= amount;
            if (CurrentTime < 0f)
                CurrentTime = 0f;

            UIManager.Instance.UpdateTimerUI(CurrentTime);
        }

        public float GetSmallTimeGainAmount() => smallTimeGainAmount;
        public float GetSmallTimePenaltyAmount() => smallTimePenaltyAmount;
        public float GetLargeTimeGainAmount() => largeTimeGainAmount;
        public float GetLargeTimePenaltyAmount() => largeTimePenaltyAmount;
        public float GetPhoneSpawnTime() => phoneSpawnTime;
        public float GetEmailBannerSpawnTime() => emailBannerSpawnTime;
    }
}

