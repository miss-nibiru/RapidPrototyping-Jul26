using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailListItem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image flagImage;
    [SerializeField] private TextMeshProUGUI senderText;

    [Header("Colors")]
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;

    private string _senderName;

    public void Initialize(string senderName)
    {
        _senderName = senderName;

        if (senderText != null)
            senderText.text = senderName;

        if (flagImage != null)
            flagImage.color = startColor;

        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        // Minimal reference to NotificationManager
        NotificationManager.Instance?.OnEmailClicked(_senderName);
    }

    // TEMP: normalized fade (0 → 1)
    public void UpdateFlagColor(float normalized)
    {
        if (flagImage != null)
            flagImage.color = Color.Lerp(startColor, endColor, normalized);
    }
}