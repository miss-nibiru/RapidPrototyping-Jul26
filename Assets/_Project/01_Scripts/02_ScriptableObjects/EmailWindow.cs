using System.Collections;
using _Project._01_Scripts._00_VisualScripts;
using TMPro;
using UnityEngine;

namespace _Project._01_Scripts._02_ScriptableObjects
{
    public class EmailWindow : MonoBehaviour
    {
    
        [SerializeField] private GameObject emailPanel;
        [SerializeField] private UIFeedbackVisual feedbackVisual;

        [Header("Email Text Fields")]
        [SerializeField] private TextMeshProUGUI senderText;
        [SerializeField] private TextMeshProUGUI subjectText;
        [SerializeField] private TextMeshProUGUI contentsText;
        [SerializeField] private TextMeshProUGUI departmentText;
        [SerializeField] private TextMeshProUGUI responseHintText;
        [SerializeField] private ExpirationColorIcon expirationIcon;

        private EmailBannerSO _currentBanner;
        private EmailBannerPanel _currentBannerPanel;
        private ResponceType _correctEmailResponceType;
        private Coroutine _windowExpirationCoroutine;
        private float _windowRemainingTime;

        public void LoadEmail(EmailBannerSO banner, EmailBannerPanel bannerPanel = null)
        {
            if (banner == null)
            {
                return;
            }
            _currentBanner = banner;
            _currentBannerPanel = bannerPanel;
            _correctEmailResponceType = banner.correctResponseType;
            if (senderText != null) senderText.text = banner.senderName;
            if (subjectText != null) subjectText.text = banner.subject;
            if (contentsText != null) contentsText.text = banner.contentsText;
            if (departmentText != null) departmentText.text = banner.department;
            if (responseHintText != null) responseHintText.text = banner.responseHint;
            _correctEmailResponceType = banner.correctResponseType;
            if (expirationIcon != null && bannerPanel != null) expirationIcon.SetBanner(bannerPanel);
        
        }

        public void OpenWindow()
        {
            if (emailPanel == null) return;
            ClearWindowUIState();
            emailPanel.SetActive(true);
            emailPanel.transform.SetAsLastSibling();
            WordBank.Instance.SpawnWords();

            if (_currentBannerPanel != null)
            {
                float fullDuration = _currentBannerPanel.bannerDuration;
                float remainingTime = _currentBannerPanel.GetRemainingTime();
                float timerDuration = Mathf.Max(remainingTime, 0.1f);
                StartWindowExpirationTimer(timerDuration);
            }
        }

        private void ClearWindowUIState()
        {
            WordBank.Instance.ClearWords();
            SlotManager.Instance.ClearSlots();
        }

        public void CloseWindow()
        {
            if (emailPanel == null)
                return;

            StopWindowExpirationTimer();
            emailPanel.SetActive(false);

            WordBank.Instance.ClearWords();
            SlotManager.Instance.ClearSlots();

            _currentBanner = null;
            _currentBannerPanel = null;
        }

        private void StartWindowExpirationTimer(float duration)
        {
            StopWindowExpirationTimer();
            _windowRemainingTime = duration;
            _windowExpirationCoroutine = StartCoroutine(WindowExpirationRoutine(duration));
        }

        private void StopWindowExpirationTimer()
        {
            if (_windowExpirationCoroutine != null)
            {
                StopCoroutine(_windowExpirationCoroutine);
                _windowExpirationCoroutine = null;
                _windowRemainingTime = 0f;
            }
        }

        private IEnumerator WindowExpirationRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (emailPanel.activeSelf)
            {
                CloseWindow();
            }
            _windowExpirationCoroutine = null;
        }

        public bool OnSendButtonClicked()
        {
            if (!SlotManager.Instance.AreSlotsFilled()) return false;
            if (_currentBanner == null) return false;
            WordObject[] words = SlotManager.Instance.GetSelectedWords();
            if (words[0] == null || words[1] == null) return false;
            bool bothCorrect = words[0].responceType == _correctEmailResponceType && words[1].responceType == _correctEmailResponceType;
            StopWindowExpirationTimer();
            if (bothCorrect)
            {
                GameManager.Instance.OnEmailCorrect();

                if (feedbackVisual != null)
                    feedbackVisual.PlaySuccessFeedback(CloseWindow);
                else
                    CloseWindow();
            }
            else
            {
                GameManager.Instance.OnEmailIncorrect();

                if (feedbackVisual != null)
                    feedbackVisual.PlayFailFeedback(CloseWindow);
                else
                    CloseWindow();
            }

            return bothCorrect;
        }

        public float GetRemainingTime()
        {
            return _windowRemainingTime;
        }

        public bool IsWindowOpen()
        {
            return emailPanel != null && emailPanel.activeSelf;
        }
    }
}
