using UnityEngine;
using UnityEditor;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Managers")]
        [SerializeField] private bool useTimer = true;
        [SerializeField] private TimeManager timerManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private UIManager uiManager;

        [Header("Audio")]
        [SerializeField] private AudioClip emailCorrectSound;
        [SerializeField] private AudioClip emailIncorrectSound;
        [SerializeField] private AudioClip phoneAnsweredSound;
        [SerializeField] private AudioClip phoneMissedSound;
        [SerializeField] private AudioClip emailExpiredSound;
        [SerializeField] private AudioClip gameoverSound;

        private bool _isPaused;
        private bool _gameOver;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            Time.timeScale = 1f;

            if (audioManager == null) audioManager = FindFirstObjectByType<AudioManager>();
            if (uiManager == null) uiManager = FindFirstObjectByType<UIManager>();
            if (timerManager == null) timerManager = FindFirstObjectByType<TimeManager>();

            audioManager?.PlayBGMusic();

            if (useTimer && timerManager != null)
                timerManager.StartTimer();
        }

        public void OnTimeExpired()
        {
            if (_gameOver) return;

            _gameOver = true;
            Time.timeScale = 0f;

            audioManager.PlayLoopingSound(gameoverSound);

            // STOP ALL SYSTEMS
            EmailBannerManager.Instance?.StopAll();
            PhoneManager.Instance?.StopAll();

            if (BrowserManager.Instance != null)
                BrowserManager.Instance.enabled = false;

            if (ComputerWindowSpawner.Instance != null)
                ComputerWindowSpawner.Instance.enabled = false;

            float elapsedTime = timerManager.GetElapsedTime();
            string scoreLabel = ScoreManager.Instance.GetScoreCategoryLabel(elapsedTime);

            UIManager.Instance.ShowLoseScreen(elapsedTime, scoreLabel);
            ScreenManager.Instance.ShowLose();
        }

        public void OnEmailIncorrect()
        {
            if (_gameOver) return;
            timerManager.SubtractTime(timerManager.GetSmallTimePenaltyAmount());
            uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
            audioManager.PlaySound(emailIncorrectSound);
        }

        public void OnEmailCorrect()
        {
            if (_gameOver) return;
            timerManager.AddTime(timerManager.GetSmallTimeGainAmount());
            uiManager.ShowBonusText("BONUS TIME!", Color.green);
            audioManager.PlaySound(emailCorrectSound);
        }

        public void OnCallAnswered()
        {
            if (_gameOver) return;
            timerManager.AddTime(timerManager.GetLargeTimeGainAmount());
            uiManager.ShowBonusText("BONUS TIME!", Color.green);
            audioManager.PlaySound(phoneAnsweredSound);
        }

        public void OnCallMissed()
        {
            if (_gameOver) return;
            timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
            uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
            audioManager.PlaySound(phoneMissedSound);
        }

        public void OnEmailBannerMissed()
        {
            if (_gameOver) return;
            timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
            uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
            audioManager.PlaySound(emailExpiredSound);
        }

        public void Pause()
        {
            if (_gameOver) return;
            _isPaused = true;
            Time.timeScale = 0f;
            ScreenManager.Instance.ShowPause();
        }

        public void OnResume()
        {
            if (_gameOver) return;
            _isPaused = false;
            Time.timeScale = 1f;
            ScreenManager.Instance.ShowGameplay();
        }

        public void TogglePause()
        {
            if (_gameOver) return;
            if (_isPaused)
                OnResume();
            else
                Pause();
        }

        public void OnQuit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}


