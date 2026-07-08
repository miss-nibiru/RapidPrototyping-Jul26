using TMPro;
using UnityEngine;
using System.Collections;
namespace _Project._01_Scripts._00_VisualScripts
{
    public class UIManager : MonoBehaviour
    {
            public static UIManager Instance;
            [SerializeField] private TextMeshProUGUI timerText;
            [SerializeField] private GameObject pauseMenu;
            
            [Header("email Notification")]
            [SerializeField] private GameObject emailNotificationUI;
            [SerializeField] private TextMeshProUGUI emailText;
            
            [Header("phoneCall Notification")]
            [SerializeField] private GameObject phoneNotificationUI;
            [SerializeField] private TextMeshProUGUI callerNameText;
            
            [Header("Bonus Notification")]
            [SerializeField] private TextMeshProUGUI bonusText;
            [SerializeField] private float bonusDisplayDuration = 1.5f;
            
            [Header("Penalty Notification")]
            [SerializeField] private TextMeshProUGUI penaltyText;
            [SerializeField] private float penaltyDisplayDuration = 1.5f;
            
            [SerializeField] private TimeManager timerManager;
            private string _defaultEmailText;
            private string _defaultPhoneText;
            private Coroutine _bonusCoroutine;
            private Coroutine _penaltyCoroutine;
            private Coroutine _phoneCallCoroutine;
            
            private void Awake()
            {
                if (Instance != null && Instance != this)
                {
                    Destroy(this);
                    return;
                }
                Instance = this;
            }
            private void Start()
            {
                if (emailNotificationUI != null)
                    emailNotificationUI.SetActive(false);
                if (emailText != null)
                    _defaultEmailText = emailText.text;
                if (bonusText != null)
                    bonusText.gameObject.SetActive(false);
                if (penaltyText != null)
                    penaltyText.gameObject.SetActive(false);
                if (timerManager == null) timerManager = FindFirstObjectByType<TimeManager>();
            }
            
            public void UpdateTimerUI(float currentTime)
            {
                int minutes = Mathf.FloorToInt(currentTime / 60f);
                int seconds = Mathf.FloorToInt(currentTime % 60f);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }
            
            public void ShowPauseMenu(bool show)
            {
                pauseMenu.SetActive(show);
            }
            
            public void ShowEmail(string message)
            {
                emailNotificationUI.SetActive(true);
                emailText.text = message;
            }
            
            public void HideEmail()
            {
                emailNotificationUI.SetActive(false);
                emailText.text = _defaultEmailText;
            }
            
            public void ShowCall(PhoneObject data)
            {
                phoneNotificationUI.SetActive(true);
                if (callerNameText != null)
                    callerNameText.text = data.callerName;
            }
            
            public void HideCall()
            {
                phoneNotificationUI.SetActive(false);
                if (callerNameText != null)
                    callerNameText.text = "";
            }
            
            public void ShowBonusText(string text, Color color)
            {
                if (bonusText == null)
                {
                    return;
                }
                if (_bonusCoroutine != null)
                    StopCoroutine(_bonusCoroutine);
                _bonusCoroutine = StartCoroutine(BonusTextRoutine(text, color));
            }

            private IEnumerator BonusTextRoutine(string text, Color color)
            {
                bonusText.text = text;
                bonusText.color = color;
                bonusText.gameObject.SetActive(true);
                yield return new WaitForSeconds(bonusDisplayDuration);
                bonusText.gameObject.SetActive(false);
                _bonusCoroutine = null;
            }
            
            public void ShowPenaltyText(string text, Color color)
            {
                if (penaltyText == null)
                {
                    return;
                }
                if (_penaltyCoroutine != null)
                    StopCoroutine(_penaltyCoroutine);
                _penaltyCoroutine = StartCoroutine(PenaltyTextRoutine(text, color));
            }

            private IEnumerator PenaltyTextRoutine(string text, Color color)
            {
                penaltyText.text = text;
                penaltyText.color = color;
                penaltyText.gameObject.SetActive(true);
                yield return new WaitForSeconds(penaltyDisplayDuration);
                penaltyText.gameObject.SetActive(false);
                _penaltyCoroutine = null;
            }
        }
    }