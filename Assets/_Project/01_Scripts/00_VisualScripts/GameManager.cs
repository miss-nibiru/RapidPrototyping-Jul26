using _Project._01_Scripts._00_VisualScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
        [Header("Amounts")]
        [SerializeField] private float timepenaltyAmount;
        [SerializeField] private float timeGainAmount;
        
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
            //time penalty logic
        }
    
        public void OnEmailCorrect()
        {
            //either time gain or something else
        }
    
        public void OnCallAnswered()
        {
            //either time gain or something else
        }
        
        public void OnCallMissed()
        {
            //time penalty logic
        }
    
        public void Pause()
        {
            _isPaused = true;

            Time.timeScale = 0f;

            // Show mouse for UI interaction
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

        private void OnQuit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
