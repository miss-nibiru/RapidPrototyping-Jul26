using _Project._01_Scripts._00_VisualScripts;
using TMPro;
using UnityEngine;

namespace _Project._01_Scripts._02_ScriptableObjects
{
    public class EmailBannerPanel : MonoBehaviour
    {
        
        public float bannerDuration = 5f;
        public float timePenalty = 10f;
        
        [SerializeField] private TextMeshProUGUI bannerSenderText;
        [SerializeField] private TextMeshProUGUI bannerSummaryText;
        [SerializeField] private TextMeshProUGUI bannerDepartmentText;
        [SerializeField] private UIFeedbackVisual feedbackVisual;
        
        public EmailBannerSO emailBannerSo;
        private float _spawnTime;

        private void Awake()
        {
            _spawnTime = Time.time;
        }

        public void InitializeBanner(EmailBannerSO banner)
        {
            emailBannerSo = banner;
            bannerDuration = emailBannerSo.bannerDuration;
            timePenalty = emailBannerSo.timePenalty;
            bannerSenderText.text = emailBannerSo.senderName;
            bannerSummaryText.text = emailBannerSo.subject;
            bannerDepartmentText.text = emailBannerSo.department;
            
            _spawnTime = Time.time;
        }
        
        public float GetRemainingTime()
        {
            float elapsedTime = Time.time - _spawnTime;
            float remaining = bannerDuration - elapsedTime;
            if (remaining < 0f)
                remaining = 0f;
            
            return remaining;
        }
        
        public float GetElapsedTime()
        {
            return Time.time - _spawnTime;
        }
        public void PlaySuccessFeedbackThenDestroy()
        {
            if (feedbackVisual != null)
            {
                feedbackVisual.PlaySuccessFeedback(ClearBanner);
            }
            else
            {
                ClearBanner();
            }
        }

        public void PlayFailFeedbackThenDestroy()
        {
            if (feedbackVisual != null)
            {
                feedbackVisual.PlayFailFeedback(ClearBanner);
            }
            else
            {
                ClearBanner();
            }
        }

        public void ClearBanner()
        {
            Destroy(gameObject);
        }
    }
}
