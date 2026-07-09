using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class PhoneManager : MonoBehaviour
    {
        public static PhoneManager Instance { get; private set; }

        [Header("Phone Call Types")]
        [SerializeField] private List<PhoneObject> phoneCalls = new();
        [SerializeField] private AudioClip ringSound;
        

        private Coroutine _phoneRoutine;
        private bool _callActive;
        private PhoneObject _currentCall;
        private AudioManager audioManager;

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
                audioManager = AudioManager.Instance;
                _currentCall = phoneCalls[Random.Range(0, phoneCalls.Count)];
                _callActive = true;
                UIManager.Instance.ShowCall(_currentCall);
                audioManager.PlayLoopingSound(ringSound);
                yield return new WaitForSeconds(_currentCall.ringDuration);
                if (_callActive)
                {
                    GameManager.Instance.OnCallMissed();
                    AudioManager.Instance.StopLoopingSound();
                    TimeManager.Instance.SubtractTime(_currentCall.timePenalty);
                    UIManager.Instance.HideCall();
                }
                _callActive = false;
            }
        }

        public void AnswerCall()
        {
            if (!_callActive) return;
            _callActive = false;
            AudioManager.Instance.StopLoopingSound();
            UIManager.Instance.HideCall();
            GameManager.Instance.OnCallAnswered();
            TimeManager.Instance.AddTime(_currentCall.timeBonus);
        }
    }
}