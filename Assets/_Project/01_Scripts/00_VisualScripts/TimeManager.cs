using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        [Header("Game Time")]
        [SerializeField] private float gameTime = 120f;
        [SerializeField] private bool countDown = true;

        [Header("Phone Timing")]
        [SerializeField] private float phoneSpawnTime;

        [Header("Email Timing")]
        [SerializeField] private float emailBannerSpawnTime;

        [Header("Bonus/Penalty Amounts")]
        [SerializeField] private float smallTimeGainAmount = 3f;
        [SerializeField] private float smallTimePenaltyAmount = 5f;
        [SerializeField] private float largeTimeGainAmount = 8f;
        [SerializeField] private float largeTimePenaltyAmount = 10f;

        [Header("Time Feedback")]
        [SerializeField] private TextMeshProUGUI clockText;
        [SerializeField] private RectTransform timeFeedbackPopup;
        [SerializeField] private Image timeFeedbackImage;
        [SerializeField] private TextMeshProUGUI timeChangeText;

        [SerializeField] private Color normalClockColor = Color.white;
        [SerializeField] private Color gainedTimeColor = Color.green;
        [SerializeField] private Color lostTimeColor = Color.red;

        [SerializeField] private float clockFlashDuration = 0.45f;
        [SerializeField] private float popupDuration = 0.4f;
        [SerializeField] private float popupMoveDistance = 35f;

        public float CurrentTime { get; private set; }

        private bool _isRunning;
        private Coroutine _clockFeedbackCoroutine;
        private Coroutine _timePopupCoroutine;
        private Vector2 _popupStartPosition;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            if (timeFeedbackPopup != null)
            {
                _popupStartPosition = timeFeedbackPopup.anchoredPosition;
                timeFeedbackPopup.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            float delta = Time.deltaTime;

            if (countDown)
            {
                CurrentTime -= delta;

                if (CurrentTime <= 0f)
                {
                    CurrentTime = 0f;
                    _isRunning = false;

                    UpdateTimerUI();
                    GameManager.Instance.OnTimeExpired();
                    return;
                }
            }
            else
            {
                CurrentTime += delta;
            }

            UpdateTimerUI();
        }

        public void StartTimer()
        {
            CurrentTime = countDown ? gameTime : 0f;
            _isRunning = true;

            UpdateTimerUI();
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void AddTime(float amount)
        {
            if (amount <= 0f)
            {
                return;
            }

            CurrentTime += amount;

            UpdateTimerUI();
            ShowTimeFeedback(amount, true);
        }

        public void SubtractTime(float amount)
        {
            if (amount <= 0f)
            {
                return;
            }

            CurrentTime = Mathf.Max(0f, CurrentTime - amount);

            UpdateTimerUI();
            ShowTimeFeedback(amount, false);
        }

        private void UpdateTimerUI()
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateTimerUI(CurrentTime);
            }
        }

        private void ShowTimeFeedback(float amount, bool gainedTime)
        {
            Color feedbackColor = gainedTime
                ? gainedTimeColor
                : lostTimeColor;

            if (clockText != null)
            {
                if (_clockFeedbackCoroutine != null)
                {
                    StopCoroutine(_clockFeedbackCoroutine);
                }

                _clockFeedbackCoroutine =
                    StartCoroutine(FlashClockColor(feedbackColor));
            }

            if (timeFeedbackPopup == null ||
                timeFeedbackImage == null ||
                timeChangeText == null)
            {
                return;
            }

            if (_timePopupCoroutine != null)
            {
                StopCoroutine(_timePopupCoroutine);
            }

            string symbol = gainedTime ? "+" : "-";

            timeChangeText.text = $"{symbol}{amount:0}";
            timeChangeText.color = feedbackColor;

            _timePopupCoroutine =
                StartCoroutine(ShowTimeChangePopup(feedbackColor));
        }

        private IEnumerator FlashClockColor(Color feedbackColor)
        {
            clockText.color = feedbackColor;

            yield return new WaitForSeconds(clockFlashDuration);

            clockText.color = normalClockColor;
            _clockFeedbackCoroutine = null;
        }

        private IEnumerator ShowTimeChangePopup(Color feedbackColor)
        {
            timeFeedbackPopup.gameObject.SetActive(true);
            timeFeedbackPopup.anchoredPosition = _popupStartPosition;

            Color textColor = feedbackColor;
            textColor.a = 1f;
            timeChangeText.color = textColor;

            Color imageColor = timeFeedbackImage.color;
            imageColor.a = 1f;
            timeFeedbackImage.color = imageColor;

            float elapsed = 0f;

            while (elapsed < popupDuration)
            {
                elapsed += Time.deltaTime;

                float progress = Mathf.Clamp01(elapsed / popupDuration);

                timeFeedbackPopup.anchoredPosition =
                    _popupStartPosition +
                    Vector2.up * popupMoveDistance * progress;

                textColor.a = 1f - progress;
                timeChangeText.color = textColor;

                imageColor.a = 1f - progress;
                timeFeedbackImage.color = imageColor;

                yield return null;
            }

            timeFeedbackPopup.anchoredPosition = _popupStartPosition;
            timeFeedbackPopup.gameObject.SetActive(false);

            _timePopupCoroutine = null;
        }

        public float GetSmallTimeGainAmount() => smallTimeGainAmount;
        public float GetSmallTimePenaltyAmount() => smallTimePenaltyAmount;
        public float GetLargeTimeGainAmount() => largeTimeGainAmount;
        public float GetLargeTimePenaltyAmount() => largeTimePenaltyAmount;
        public float GetPhoneSpawnTime() => phoneSpawnTime;
        public float GetEmailBannerSpawnTime() => emailBannerSpawnTime;
    }
}