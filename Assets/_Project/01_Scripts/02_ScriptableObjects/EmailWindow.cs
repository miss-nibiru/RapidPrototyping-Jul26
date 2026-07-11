using _Project._01_Scripts._00_VisualScripts;
using TMPro;
using UnityEngine;
using System.Collections;
using _Project._01_Scripts._02_ScriptableObjects;

public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;
    [Header("Email Text Fields")]
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI subjectText;
    [SerializeField] private TextMeshProUGUI contentsText;
    
    private EmailBannerSO _currentBanner;
    private EmailBannerPanel _currentBannerPanel;
    private responceType correctEmailResponceType;
    private Coroutine _windowExpirationCoroutine;
    private float _windowRemainingTime;
    
    public void LoadEmail(EmailBannerSO banner, EmailBannerPanel bannerPanel = null)
    {
        if (banner == null)
        {
            return;
        }
        _currentBanner = banner;
        _currentBannerPanel = bannerPanel;
        
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
        if (_currentBannerPanel != null)
        {
            float fullDuration = _currentBannerPanel.bannerDuration;
            float remainingTime = _currentBannerPanel.GetRemainingTime();
            float timerDuration = Mathf.Max(remainingTime, 0.1f);  
            StartWindowExpirationTimer(timerDuration);
        }
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
        StopWindowExpirationTimer();
        emailPanel.SetActive(false);
        WordBank.Instance.ClearWords();
        SlotManager.Instance.ClearSlots();
        _currentBanner = null;
        _currentBannerPanel = null;
    }
    
    private void StartWindowExpirationTimer(float duration)
    {
        StopWindowExpirationTimer();
        _windowRemainingTime = duration;
        _windowExpirationCoroutine = StartCoroutine(WindowExpirationRoutine(duration));
    }

    private void StopWindowExpirationTimer()
    {
        if (_windowExpirationCoroutine != null)
        {
            StopCoroutine(_windowExpirationCoroutine);
            _windowExpirationCoroutine = null;
            _windowRemainingTime = 0f;
        }
    }
    private IEnumerator WindowExpirationRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (emailPanel.activeSelf)
        {
            CloseWindow();
        }

        _windowExpirationCoroutine = null;
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
        StopWindowExpirationTimer();
        CloseWindow();
    }
    public float GetRemainingTime()
    {
        return _windowRemainingTime;
    }
    public bool IsWindowOpen()
    {
        return emailPanel != null && emailPanel.activeSelf;
    }
}