using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFeedbackVisual : MonoBehaviour
{
    [Header("Feedback Images")]
    [SerializeField] private Image feedbackTintImage;
    [SerializeField] private Image feedbackIconImage;

    [Header("Sprites")]
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    [Header("Colors")]
    [SerializeField] private Color successColor = new Color(0f, 1f, 0f, 0.35f);
    [SerializeField] private Color failColor = new Color(1f, 0f, 0f, 0.35f);

    [Header("Timing")]
    [SerializeField] private float feedbackDuration;

    private Coroutine _feedbackCoroutine;

    private void Awake()
    {
        HideFeedback();
    }

    public void PlaySuccessFeedback(Action onComplete = null)
    {
        PlayFeedback(successSprite, successColor, onComplete);
    }

    public void PlayFailFeedback(Action onComplete = null)
    {
        PlayFeedback(failSprite, failColor, onComplete);
    }

    private void PlayFeedback(Sprite iconSprite, Color tintColor, Action onComplete)
    {
        if (_feedbackCoroutine != null) StopCoroutine(_feedbackCoroutine);
        _feedbackCoroutine = StartCoroutine(FeedbackRoutine(iconSprite, tintColor, onComplete));
    }

    private IEnumerator FeedbackRoutine(Sprite iconSprite, Color tintColor, Action onComplete)
    {
        if (!feedbackTintImage)
        {
            feedbackTintImage.gameObject.SetActive(true);
            feedbackTintImage.color = tintColor;
        }

        if (!feedbackIconImage)
        {
            feedbackIconImage.gameObject.SetActive(true);
            feedbackIconImage.sprite = iconSprite;
        }

        yield return new WaitForSeconds(feedbackDuration);

        HideFeedback();

        onComplete?.Invoke();

        _feedbackCoroutine = null;
    }

    private void HideFeedback()
    {
        if (!feedbackTintImage)
            feedbackTintImage.gameObject.SetActive(false);

        if (!feedbackIconImage)
            feedbackIconImage.gameObject.SetActive(false);
    }
}