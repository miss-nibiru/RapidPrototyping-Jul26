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
            if (_currentBannerSO == null || _bannerExpired)
                return;

            emailWindow.LoadEmail(_currentBannerSO, _currentBannerPanel);
            emailWindow.OpenWindow();
        }

        public void OnSendButtonClicked()
        {
            // No active banner or already expired → do nothing
            if (_currentBannerSO == null || _currentBannerPanel == null || _bannerExpired)
                return;

            // ❗ HARD GATE: you cannot send unless both slots are filled
            if (!SlotManager.Instance.AreSlotsFilled())
            {
                Debug.Log("[EmailController] Send blocked — both slots must be filled.");
                return;
            }

            // EmailWindow now knows slots are filled; it will:
            // - grade the email
            // - play success/fail feedback
            // - close itself via feedback callback
            bool wasSuccessful = emailWindow.OnSendButtonClicked();

            // Banner is handled (replied to) either way: success or fail
            EmailBannerManager.Instance.OnBannerHandled(_currentBannerPanel, wasSuccessful);

            // Reset controller state so the next banner works cleanly
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
