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

    private bool _isActive;

    private void Awake()
    {
        if (iconImage == null)
            iconImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnDisable()
    {
        _isActive = false;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (bannerPanel == null || iconImage == null)
            return;

        float remaining = bannerPanel.GetRemainingTime();
        float duration = bannerPanel.bannerDuration;

        if (duration <= 0f)
            return;

        float normalized = Mathf.Clamp01(1f - (remaining / duration));

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
        _isActive = true;
    }

    /*public void ClearBanner()
    {
        bannerPanel = null;
        _isActive = false;
    }*/
}

