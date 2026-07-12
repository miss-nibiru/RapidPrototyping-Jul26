using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class LoseScreenPanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button quitButton;
        [SerializeField] private Button playAgainButton;

        [Header("Scene Settings")]
        [SerializeField] private string gameplaySceneName = "GameplayScene";
        [SerializeField] private string titleSceneName = "TitleScene";

        private void OnEnable()
        {
            // ⭐ CRITICAL LINE ⭐
            // When this panel becomes active, force it to the top
            transform.SetAsLastSibling();

            if (quitButton != null)
                quitButton.onClick.AddListener(OnQuitButtonClicked);

            if (playAgainButton != null)
                playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        }

        private void OnDisable()
        {
            if (quitButton != null)
                quitButton.onClick.RemoveListener(OnQuitButtonClicked);

            if (playAgainButton != null)
                playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClicked);
        }

        private void OnQuitButtonClicked()
        {
            Time.timeScale = 1f;
            EmailBannerManager.Instance?.StopSpawning();
            EmailBannerManager.Instance?.ClearAllBanners();
            SceneManager.LoadScene(titleSceneName);
        }

        private void OnPlayAgainButtonClicked()
        {
            Time.timeScale = 1f;
            EmailBannerManager.Instance?.StopSpawning();
            EmailBannerManager.Instance?.ClearAllBanners();
            SceneManager.LoadScene(gameplaySceneName);
        }
    }
}