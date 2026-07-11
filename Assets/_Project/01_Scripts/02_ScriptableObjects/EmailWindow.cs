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
        if (banner == null)
        {
            return;
        }
        _currentBanner = banner;
        if (senderText != null)
            senderText.text = banner.senderName;
        if (subjectText != null)
            subjectText.text = banner.subject;
        if (contentsText != null)
            contentsText.text = banner.contentsText;
        correctEmailResponceType = banner.correctResponseType;
    }
    
    public void OpenWindow()
    {
        if (emailPanel == null)
        {
            return;
        }
        ClearWindowUIState();
        emailPanel.SetActive(true);
        WordBank.Instance.SpawnWords();
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
        emailPanel.SetActive(false);
        WordBank.Instance.ClearWords();
        SlotManager.Instance.ClearSlots();
        _currentBanner = null;
    }
    
    public void OnSendButtonClicked()
    {
        if (!SlotManager.Instance.AreSlotsFilled())
        {
            return;
        }
        if (_currentBanner == null)
        {
            return;
        }
        WordObject[] words = SlotManager.Instance.GetSelectedWords();
        if (words[0] == null || words[1] == null)
        {
            Debug.LogError("[EmailWindow] Cannot send: one or more words are null");
            return;
        }
        
        bool bothCorrect =
            words[0].responceType == correctEmailResponceType &&
            words[1].responceType == correctEmailResponceType;
        
        if (bothCorrect)
        {
            GameManager.Instance.OnEmailCorrect();
        }
        else
        {
            GameManager.Instance.OnEmailIncorrect();
        }
        CloseWindow();
    }
}