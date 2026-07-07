using TMPro;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class UIManager : MonoBehaviour
    {
            public static UIManager Instance;

            [SerializeField] private TextMeshProUGUI timerText;
            [SerializeField] private GameObject pauseMenu;
            
            [SerializeField] private GameObject emailNotificationUI;
            [SerializeField] private TextMeshProUGUI emailText;
            [SerializeField] private GameObject phoneNotificationUI;
            [SerializeField] private TextMeshProUGUI phoneText;

            private string _defaultEmailText;
            private string _defaultPhoneText;

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
            
            public void ShowCall(string message)
            {
                phoneNotificationUI.SetActive(true);
                phoneText.text = message;
            }

            public void HideCall()
            {
                phoneNotificationUI.SetActive(false);
                phoneText.text = _defaultEmailText;
            }
        }
    }
