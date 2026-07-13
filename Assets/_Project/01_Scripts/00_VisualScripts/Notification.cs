using TMPro;
using UnityEngine;
using _Project._01_Scripts._00_VisualScripts;

public class Notification : MonoBehaviour
{
    [SerializeField] private GameObject notification;
    [SerializeField] private TextMeshProUGUI emailCount;

    private void Update()
    {
        if (EmailBannerManager.Instance == null)
            return;
        int activeEmailCount =
            EmailBannerManager.Instance.GetActiveBannerCount();
        bool hasActiveEmails = activeEmailCount > 0;
        notification.SetActive(hasActiveEmails);

        if (hasActiveEmails)
        {
            emailCount.text = activeEmailCount.ToString();
        }
    }
}