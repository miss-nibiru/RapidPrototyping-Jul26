using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance;

        [Header("Screens")]
        [SerializeField] private GameObject gameplayScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject loseScreen;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        public void ShowGameplay()
        {
            gameplayScreen.SetActive(true);
            pauseScreen.SetActive(false);
            loseScreen.SetActive(false);
        }

        public void ShowPause()
        {
            gameplayScreen.SetActive(false);
            pauseScreen.SetActive(true);
            loseScreen.SetActive(false);
            pauseScreen.transform.SetAsLastSibling();
        }

        public void ShowLose()
        {
            gameplayScreen.SetActive(false);
            pauseScreen.SetActive(false);
            loseScreen.SetActive(true);
            loseScreen.transform.SetAsLastSibling();
        }
    }
}

