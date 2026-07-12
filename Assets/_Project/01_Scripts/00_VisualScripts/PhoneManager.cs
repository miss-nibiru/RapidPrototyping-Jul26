using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class PhoneManager : MonoBehaviour
    {
        public static PhoneManager Instance { get; private set; }

        [Header("Phone Call Types")]
        [SerializeField] private List<PhoneObject> phoneCalls = new();
        [SerializeField] private AudioClip ringSound;
        [SerializeField] private UIFeedbackVisual callFeedbackVisual;

        private Coroutine _phoneRoutine;
        private bool _callActive;
        private PhoneObject _currentCall;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            StartPhoneLoop();
        }

        public void StartPhoneLoop()
        {
            if (_phoneRoutine != null)
                StopCoroutine(_phoneRoutine);

            _phoneRoutine = StartCoroutine(PhoneLoopRoutine());
        }

        private IEnumerator PhoneLoopRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeManager.Instance.GetPhoneSpawnTime());
                _currentCall = phoneCalls[Random.Range(0, phoneCalls.Count)];
                _callActive = true;
                UIManager.Instance.ShowCall(_currentCall);
                AudioManager.Instance?.PlayLoopingSound(ringSound);
                yield return new WaitForSeconds(_currentCall.ringDuration);
                if (_callActive)
                {
                    _callActive = false;

                    AudioManager.Instance?.StopLoopingSound();
                    GameManager.Instance.OnCallMissed();

                    if (callFeedbackVisual != null)
                    {
                        callFeedbackVisual.PlayFailFeedback(
                            UIManager.Instance.HideCall
                        );
                    }
                    else
                    {
                        UIManager.Instance.HideCall();
                    }
                    
                    _callActive = false;
                }
            }
        }

        public void AnswerCall()
        {
            HandleCallAction(PhoneObject.PhoneActionType.Answer);
        }

        public void IgnoreCall()
        {
            HandleCallAction(PhoneObject.PhoneActionType.Ignore);
        }

        private void HandleCallAction(PhoneObject.PhoneActionType playerAction)
        {
            if (!_callActive) return;
            _callActive = false;
            AudioManager.Instance?.StopLoopingSound();
            bool correctAction = _currentCall.correctAction == playerAction;
            
            if (correctAction)
            {
                GameManager.Instance.OnCallAnswered();

                if (callFeedbackVisual != null)
                {
                    callFeedbackVisual.PlaySuccessFeedback(UIManager.Instance.HideCall);
                }
                else
                {
                    UIManager.Instance.HideCall();
                }
            }
            else
            {
                GameManager.Instance.OnCallMissed();

                if (callFeedbackVisual != null)
                {
                    callFeedbackVisual.PlayFailFeedback(UIManager.Instance.HideCall);
                }
                else
                {
                    UIManager.Instance.HideCall();
                }
            }
        }
    }
}