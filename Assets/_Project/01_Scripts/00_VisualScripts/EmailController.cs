using UnityEngine;
using _Project._01_Scripts._02_ScriptableObjects;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class EmailController : MonoBehaviour
    {
        public static EmailController Instance { get; private set; }

        [SerializeField] private EmailWindow emailWindow;

        private EmailBannerSO _currentBannerSO;
        private EmailBannerPanel _currentBannerPanel;
        private bool _bannerExpired;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void SetCurrentBanner(EmailBannerPanel bannerPanel)
        {
            _currentBannerPanel = bannerPanel;
            _currentBannerSO = bannerPanel.emailBannerSO;
            _bannerExpired = false;
        }

        public void OpenCurrentEmail()
        {
            if (_currentBannerSO == null) return;
            if (_bannerExpired) return;
            emailWindow.LoadEmail(_currentBannerSO);
            emailWindow.OpenWindow();
        }

        public void OnSendButtonClicked()
        {
            if (_currentBannerSO == null) return;
            emailWindow.OnSendButtonClicked();
            EmailBannerManager.Instance.DestroyBanner();
            _bannerExpired = true;
            _currentBannerSO = null;
            _currentBannerPanel = null;
        }

        public void OnBannerExpired()
        {
            _bannerExpired = true;
            emailWindow.CloseWindow();
            _currentBannerSO = null;
            _currentBannerPanel = null;
        }
    }
}