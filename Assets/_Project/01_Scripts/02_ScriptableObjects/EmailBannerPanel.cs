using _Project._01_Scripts._00_VisualScripts;
using TMPro;
using UnityEngine;

namespace _Project._01_Scripts._02_ScriptableObjects
{
    public class EmailBannerPanel : MonoBehaviour
    {
        public string senderName;
        public string subject;
        public string previewText;
        public string contentsText;
        public float bannerDuration = 5f;
        public float timePenalty = 10f;
        
        [SerializeField] private TextMeshProUGUI bannerSenderText;
        [SerializeField] private TextMeshProUGUI bannerSubjectText;
        [SerializeField] private TextMeshProUGUI bannerPreviewText;
        
        public EmailBannerSO emailBannerSO;
        private float _spawnTime;

        private void Awake()
        {
            _spawnTime = Time.time;
        }

        public void InitializeBanner(EmailBannerSO banner)
        {
            emailBannerSO = banner;
            senderName = emailBannerSO.senderName;
            subject = emailBannerSO.subject;
            previewText = emailBannerSO.previewText;
            bannerDuration = emailBannerSO.bannerDuration;
            timePenalty = emailBannerSO.timePenalty;
            
            bannerSenderText.text = senderName;
            bannerSubjectText.text = subject;
            bannerPreviewText.text = previewText;
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

        public void ClearBanner()
        {
            Destroy(gameObject);
        }
    }
}
