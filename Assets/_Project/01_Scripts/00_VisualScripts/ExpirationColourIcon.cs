using UnityEngine;
using UnityEngine.UI;
using _Project._01_Scripts._02_ScriptableObjects;

public class ExpirationColorIcon : MonoBehaviour
{
    [Header("Target Banner")]
    [SerializeField] private EmailBannerPanel bannerPanel;

    [Header("Icon Colors")]
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color midColor = Color.yellow;
    [SerializeField] private Color endColor = Color.red;

    [Header("UI")]
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        if (iconImage == null)
            iconImage = GetComponent<Image>();
    }

    private void Update()
    {
        // If bannerPanel is null, do nothing.
        if (bannerPanel == null || iconImage == null)
            return;

        float remaining = bannerPanel.GetRemainingTime();
        float duration = bannerPanel.bannerDuration;

        if (duration <= 0f)
            return;

        float normalized = Mathf.Clamp01(1f - (remaining / duration));

        // 0 → green, 0.5 → yellow, 1 → red
        if (normalized < 0.5f)
        {
            float t = normalized / 0.5f;
            iconImage.color = Color.Lerp(startColor, midColor, t);
        }
        else
        {
            float t = (normalized - 0.5f) / 0.5f;
            iconImage.color = Color.Lerp(midColor, endColor, t);
        }
    }

    public void SetBanner(EmailBannerPanel panel)
    {
        bannerPanel = panel;
    }
}

