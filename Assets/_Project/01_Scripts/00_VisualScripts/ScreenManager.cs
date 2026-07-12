using UnityEngine;
using UnityEngine.UI;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance { get; private set; }

        [Header("Screens")]
        [SerializeField] private GameObject gameplayScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject loseScreen;

        [Header("Buttons")]
        [SerializeField] private Button unpauseButton;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            ShowGameplay();
        }

        private void OnEnable()
        {
            if (unpauseButton != null)
                unpauseButton.onClick.AddListener(OnUnpauseButton);
        }

        private void OnDisable()
        {
            if (unpauseButton != null)
                unpauseButton.onClick.RemoveListener(OnUnpauseButton);
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
        }

        public void ShowLose()
        {
            if (gameplayScreen != null)
                gameplayScreen.SetActive(false);
            if (pauseScreen != null)
                pauseScreen.SetActive(false);
            if (loseScreen != null)
                loseScreen.SetActive(true);
        }

        private void OnUnpauseButton()
        {
            GameManager.Instance?.OnResume();
        }
    }
}
