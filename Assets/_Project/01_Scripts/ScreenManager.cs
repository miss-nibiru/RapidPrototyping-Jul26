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

        public void ShowPause()
        {
            gameplayScreen.SetActive(false);
            pauseScreen.SetActive(true);
        }
    
        public void ShowLose()
        {
            gameplayScreen.SetActive(false);
            loseScreen.SetActive(true);
        }

        public void ShowGameplay()
        {
            gameplayScreen.SetActive(true);
            pauseScreen.SetActive(false);
        }

        private void OnUnpauseButton()
        {
            GameManager.Instance.OnResume();
        }
    }
}

