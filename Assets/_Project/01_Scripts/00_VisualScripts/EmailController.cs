using _Project._01_Scripts._00_VisualScripts;
using _Project._01_Scripts._02_ScriptableObjects;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class EmailController : MonoBehaviour
    {
        public static EmailController Instance { get; private set; }

        [SerializeField] private EmailWindow emailWindow;
        
        private EmailBannerPanel _currentBannerPanel;
        private EmailBannerSO _currentBannerSO;
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
            if (bannerPanel == null)
            {
                Debug.LogWarning("[EmailController] SetCurrentBanner called with null panel");
                return;
            }
            _currentBannerPanel = bannerPanel;
            _currentBannerSO = bannerPanel.emailBannerSo;
            _bannerExpired = false;
        }

        public void OpenCurrentEmail()
        {
            if (_currentBannerSO == null)
            {
                Debug.LogWarning("[EmailController] No current banner to open");
                return;
            }

            if (_bannerExpired)
            {
                return;
            }
            emailWindow.LoadEmail(_currentBannerSO, _currentBannerPanel);
            emailWindow.OpenWindow();
        }
        public void OnSendButtonClicked()
        {
            
            if (_currentBannerSO == null) return;
            bool wasSuccessful = emailWindow.OnSendButtonClicked();
            EmailBannerManager.Instance.OnBannerHandled(_currentBannerPanel, wasSuccessful);
            ResetEmailState();
            
        }
        public void OnBannerExpired()
        {
            if (_currentBannerSO != null)
            {
                Debug.Log($"[EmailController] Banner from {_currentBannerSO.senderName} expired - closing window");
            }
            emailWindow.CloseWindow();
            ResetEmailState();
        }
        private void ResetEmailState()
        {
            _bannerExpired = true;
            _currentBannerSO = null;
            _currentBannerPanel = null;
        }
        public EmailBannerPanel GetCurrentBannerPanel()
        {
            return _currentBannerPanel;
        }
        public bool HasActiveBanner()
        {
            return _currentBannerSO != null && !_bannerExpired;
        }
    }
}