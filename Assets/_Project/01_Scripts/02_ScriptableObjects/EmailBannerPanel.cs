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
        public float bannerDuration = 5f;   
        public float timeBonus = 8f;        
        public float timePenalty = 10f;
        [SerializeField] private TextMeshProUGUI bannerSenderText;
        [SerializeField] private TextMeshProUGUI bannerSubjectText;
        [SerializeField] private TextMeshProUGUI bannerPreviewText;
        public EmailBannerSO emailBannerSO;

        public void Initialize(EmailBannerSO banner)
        {
            emailBannerSO = banner;
            senderName = emailBannerSO.senderName;
            subject = emailBannerSO.subject;
            previewText = emailBannerSO.previewText;
            bannerDuration = emailBannerSO.bannerDuration;
            timeBonus = emailBannerSO.timeBonus;
            timePenalty = emailBannerSO.timePenalty;
            
            bannerSenderText.text = senderName;
            bannerSubjectText.text = subject;
            bannerPreviewText.text = previewText;
        }

        public void ClearBanner()
        {
            Destroy(gameObject);
        }
    }
}

