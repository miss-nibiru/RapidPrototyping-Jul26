using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            EmailBannerManager.Instance?.StopAll();
            PhoneManager.Instance?.StopAll();
            if (BrowserManager.Instance != null)
                BrowserManager.Instance.enabled = false;
            if (ComputerWindowSpawner.Instance != null)
                ComputerWindowSpawner.Instance.enabled = false;
            float elapsedTime = timerManager.GetSurvivalTime();
            string scoreLabel = ScoreManager.Instance.GetScoreCategoryLabel(elapsedTime);
            if (uiManager != null)
                uiManager.ShowLoseScreen(elapsedTime, scoreLabel);
            if (ScreenManager.Instance != null)
                ScreenManager.Instance.ShowLose();
        }
        
        public void WinGame()
        {
            
            if (_gameOver) return;
            _gameOver = true;
            
            Time.timeScale = 1f;
            EmailBannerManager.Instance?.StopAll();
            PhoneManager.Instance?.StopAll();
            SceneManager.LoadScene("03_WINGAME");
            
        }

        public void OnEmailIncorrect()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetSmallTimePenaltyAmount());
                if (uiManager != null)
                    uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
                if (audioManager != null)
                    audioManager.PlaySound(emailIncorrectSound);
            }
        }

        public void OnEmailCorrect()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.AddTime(timerManager.GetSmallTimeGainAmount());
                if (uiManager != null)
                    uiManager.ShowBonusText("BONUS TIME!", Color.green);
                if (audioManager != null)
                    audioManager.PlaySound(emailCorrectSound);
            }
        }
        public void OnCallAnswered()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.AddTime(timerManager.GetLargeTimeGainAmount());
                if (uiManager != null)
                    uiManager.ShowBonusText("BONUS TIME!", Color.green);
                if (audioManager != null)
                    audioManager.PlaySound(phoneAnsweredSound);
            }
        }

        public void OnCallMissed()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
                if (uiManager != null)
                    uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
                if (audioManager != null)
                    audioManager.PlaySound(phoneMissedSound);
            }
        }
        /*public void OnEmailBannerMissed()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
                if (uiManager != null)
                    uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
                if (audioManager != null)
                    audioManager.PlaySound(emailExpiredSound);
            }
        }*/

        private void Pause()
        {
            if (_gameOver) return;
            _isPaused = true;
            Time.timeScale = 0f;
            if (ScreenManager.Instance != null)
                ScreenManager.Instance.ShowPause();
        }

        public void OnResume()
        {
            if (_gameOver) return;
            _isPaused = false;
            Time.timeScale = 1f;
            if (ScreenManager.Instance != null)
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
/*
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            Time.timeScale = 1f;
            EmailBannerManager.Instance?.StopAll();
            PhoneManager.Instance?.StopAll();
            SceneManager.LoadScene("04_LOSEGAME");
            
        }

        public void OnEmailIncorrect()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetSmallTimePenaltyAmount());
                if (uiManager != null)
                    uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
                if (audioManager != null)
                    audioManager.PlaySound(emailIncorrectSound);
            }
        }

        public void OnEmailCorrect()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.AddTime(timerManager.GetSmallTimeGainAmount());
                if (uiManager != null)
                    uiManager.ShowBonusText("BONUS TIME!", Color.green);
                if (audioManager != null)
                    audioManager.PlaySound(emailCorrectSound);
            }
        }
        public void OnCallAnswered()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.AddTime(timerManager.GetLargeTimeGainAmount());
                if (uiManager != null)
                    uiManager.ShowBonusText("BONUS TIME!", Color.green);
                if (audioManager != null)
                    audioManager.PlaySound(phoneAnsweredSound);
            }
        }

        public void OnCallMissed()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
                if (uiManager != null)
                    uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
                if (audioManager != null)
                    audioManager.PlaySound(phoneMissedSound);
            }
        }
        public void OnEmailBannerMissed()
        {
            if (_gameOver) return;
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
                if (uiManager != null)
                    uiManager.ShowPenaltyText("TIME PENALTY", Color.red);
                if (audioManager != null)
                    audioManager.PlaySound(emailExpiredSound);
            }
        }

        private void Pause()
        {
            if (_gameOver) return;
            _isPaused = true;
            Time.timeScale = 0f;
            if (ScreenManager.Instance != null)
                ScreenManager.Instance.ShowPause();
        }

        public void OnResume()
        {
            if (_gameOver) return;
            _isPaused = false;
            Time.timeScale = 1f;
            if (ScreenManager.Instance != null)
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

        public void WinGame()
        {
            
            if (_gameOver) return;
            _gameOver = true;
            
            Time.timeScale = 1f;
            EmailBannerManager.Instance?.StopAll();
            PhoneManager.Instance?.StopAll();
            SceneManager.LoadScene("03_WINGAME");
            
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
*/


