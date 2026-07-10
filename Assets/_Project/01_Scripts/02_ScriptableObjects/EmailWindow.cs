using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;
using TMPro;

public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;

    [Header("Email Text Fields")]
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI subjectText;
    [SerializeField] private TextMeshProUGUI contentsText;

    private EmailBannerSO _currentBanner;
    private responceType correctEmailResponceType;

    public void LoadEmail(EmailBannerSO banner)
    {
        _currentBanner = banner;

        senderText.text = banner.senderName;
        subjectText.text = banner.subject;
        contentsText.text = banner.contentsText;

        correctEmailResponceType = banner.correctResponseType;   // NEW
    }

    public void OpenWindow()
    {
        emailPanel.SetActive(true);
        WordBank.Instance.SpawnWords();
    }

    public void CloseWindow()
    {
        emailPanel.SetActive(false);
        WordBank.Instance.ClearWords();
        SlotManager.Instance.ClearSlots();
    }

    public void OnSendButtonClicked()
    {
        if (!SlotManager.Instance.AreSlotsFilled())
            return;

        WordObject[] words = SlotManager.Instance.GetSelectedWords();

        bool bothCorrect =
            words[0].responceType == correctEmailResponceType &&
            words[1].responceType == correctEmailResponceType;

        if (bothCorrect)
            GameManager.Instance.OnEmailCorrect();
        else
            GameManager.Instance.OnEmailIncorrect();

        CloseWindow();
    }
}