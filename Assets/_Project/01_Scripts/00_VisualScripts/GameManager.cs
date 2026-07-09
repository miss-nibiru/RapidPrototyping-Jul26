using _Project._01_Scripts._00_VisualScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
        
        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;

        [Header("Managers")]
        [SerializeField] private bool useTimer = true;
        [SerializeField] private TimeManager timerManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private UIManager uiManager;

        private bool _isPaused;

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

        private void OnEnable()
        {
            if (resumeButton != null)
                resumeButton.onClick.AddListener(OnResume);

            if (quitButton != null)
                quitButton.onClick.AddListener(OnQuit);
        }

        private void OnDisable()
        {
            if (resumeButton != null)
                resumeButton.onClick.RemoveListener(OnResume);

            if (quitButton != null)
                quitButton.onClick.RemoveListener(OnQuit);
        }
        
    
        public void OnEmailIncorrect()
        {
            ApplySmallTimePenalty();
        }
    
        public void OnEmailCorrect()
        {
            ApplySmallTimeBonus();
        }
    
        public void OnCallAnswered()
        {
            ApplyLargeTimeBonus();
        }
        
        public void OnCallMissed()
        {
            ApplyLargeTimePenalty();
        }
    
        public void Pause()
        {
            _isPaused = true;
            Time.timeScale = 0f;
            ScreenManager.Instance.ShowPause();
        }

        public void OnResume()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            ScreenManager.Instance.ShowGameplay();
        }

        public void TogglePause()
        {
            if (_isPaused)
                OnResume();
            else
                Pause();
        }

        public void OnTimeExpired()
        {
            Debug.Log("Timer expired");
            //logic for the rest of the game and a lose screen
        }
    

        public void ApplySmallTimeBonus()
        {
            if (timerManager != null)
            {
                timerManager.AddTime(timerManager.GetSmallTimeGainAmount());
                uiManager?.ShowBonusText("BONUS TIME!", Color.green);
                audioManager?.PlaySound(null); // TODO: Add bonus SFX
            }
        }

        public void ApplySmallTimePenalty()
        {
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetSmallTimePenaltyAmount());
                uiManager?.ShowPenaltyText("TIME PENALTY", Color.red);
                audioManager?.PlaySound(null); // TODO: Add penalty SFX
            }
        }
        
        public void ApplyLargeTimeBonus()
        {
            if (timerManager != null)
            {
                timerManager.AddTime(timerManager.GetLargeTimeGainAmount());
                uiManager?.ShowBonusText("BONUS TIME!", Color.green);
                audioManager?.PlaySound(null); // TODO: Add bonus SFX
            }
        }

        public void ApplyLargeTimePenalty()
        {
            if (timerManager != null)
            {
                timerManager.SubtractTime(timerManager.GetLargeTimePenaltyAmount());
                uiManager?.ShowPenaltyText("TIME PENALTY", Color.red);
                audioManager?.PlaySound(null); // TODO: Add penalty SFX
            }
        }

        private void OnQuit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }