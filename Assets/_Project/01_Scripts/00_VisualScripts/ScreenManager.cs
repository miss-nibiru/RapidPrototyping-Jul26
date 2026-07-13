using UnityEngine;
using UnityEngine.UI;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance;
        
        [Header("Screens")]
        [SerializeField] private GameObject gameplayScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject loseScreen;
        
        [Header("Pause Menu Buttons")]
        [SerializeField] private Button unpauseButton;
        [SerializeField] private Button pauseQuitButton;
        
        [Header("sounds")]
        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private AudioClip pauseSound;
        

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            if (unpauseButton != null)
                unpauseButton.onClick.AddListener(OnUnpauseButtonClicked);
            if (pauseQuitButton != null)
                pauseQuitButton.onClick.AddListener(OnPauseQuitButtonClicked);
        }

        private void OnDisable()
        {
            if (unpauseButton != null)
                unpauseButton.onClick.RemoveListener(OnUnpauseButtonClicked);
            if (pauseQuitButton != null)
                pauseQuitButton.onClick.RemoveListener(OnPauseQuitButtonClicked);
        }

        public void ShowGameplay()
        {
            if (gameplayScreen != null)
                gameplayScreen.SetActive(true);
            if (pauseScreen != null)
                pauseScreen.SetActive(false);
            if (loseScreen != null)
                loseScreen.SetActive(false);
        }

        public void ShowPause()
        {
            if (gameplayScreen != null)
                gameplayScreen.SetActive(false);
            if (pauseScreen != null)
                pauseScreen.SetActive(true);
            if (loseScreen != null)
                loseScreen.SetActive(false);
            if (pauseScreen != null)
                pauseScreen.transform.SetAsLastSibling();
            AudioManager.Instance.StopLoopingSound();
            AudioManager.Instance.PlaySound(pauseSound);
        }

        public void ShowLose()
        {
            if (gameplayScreen != null)
                gameplayScreen.SetActive(false);
            if (pauseScreen != null)
                pauseScreen.SetActive(false);
            if (loseScreen != null)
                loseScreen.SetActive(true);
            if (loseScreen != null)
                loseScreen.transform.SetAsLastSibling();
            AudioManager.Instance.StopBGMusic();
            AudioManager.Instance.PlaySound(gameOverSound);
           
        }
        private void OnUnpauseButtonClicked()
        {
            GameManager.Instance?.OnResume();
        }
        private void OnPauseQuitButtonClicked()
        {
            GameManager.Instance?.OnQuit();
        }
    }
}