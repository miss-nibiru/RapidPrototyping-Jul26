using _Project._01_Scripts._00_VisualScripts;
using _Project._01_Scripts._02_ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class EmailBannerClick : MonoBehaviour
{
    private EmailBannerPanel _bannerPanel;

    private void Awake()
    {
        _bannerPanel = GetComponent<EmailBannerPanel>();
        if (_bannerPanel == null)
            _bannerPanel = GetComponentInParent<EmailBannerPanel>();

        Button btn = GetComponent<Button>();
        if (btn == null)
            btn = GetComponentInChildren<Button>();

        if (btn != null)
        {
            btn.onClick.AddListener(OnBannerClicked);
        }
        else
        {
            Debug.LogWarning("[EmailBannerClick] No Button found on banner object!");
        }
    }

    private void OnBannerClicked()
    {
        if (_bannerPanel == null)
        {
            Debug.LogWarning("[EmailBannerClick] No EmailBannerPanel found on click");
            return;
        }
        EmailController.Instance.SetCurrentBanner(_bannerPanel);
        EmailController.Instance.OpenCurrentEmail();
    }
}