using System.Collections;
using System.Collections.Generic;
using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;

public class EmailBannerManager : MonoBehaviour
{
    public static EmailBannerManager Instance { get; private set; }

        [Header("banner Types")]
        [SerializeField] private List<EmailBannerObject> emailBanners = new();
        
        private Coroutine _bannerRoutine;
        private bool _bannerActive;
        private EmailBannerObject _currentBanner;

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
            StartBannerLoop();
        }

        public void StartBannerLoop()
        {
            if (_bannerRoutine != null)
                StopCoroutine(_bannerRoutine);

            _bannerRoutine = StartCoroutine(bannerLoopRoutine());
        }

        private IEnumerator bannerLoopRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeManager.Instance.GetEmailBannerSpawnTime());
                _currentBanner =  emailBanners[Random.Range(0, emailBanners.Count)];
                _bannerActive = true;
                UIManager.Instance.ShowEmailBanner(_currentBanner);
                yield return new WaitForSeconds(_currentBanner.bannerDuration);
                if (_bannerActive)
                {
                    GameManager.Instance.OnEmailBannerMissed();
                    TimeManager.Instance.SubtractTime(_currentBanner.timePenalty);
                    UIManager.Instance.HideEmailBanner();
                }
                _bannerActive = false;
            }
        }

        public void AnswerCall()
        {
            if (!_bannerActive) return;
            _bannerActive = false;
            UIManager.Instance.HideEmailBanner(); 
            GameManager.Instance.OnEmailBannerOpened();
            TimeManager.Instance.AddTime(_currentBanner.timeBonus);
        }
    }

//I have a possible edge case now because if I can be able to bring back up the email again if I want to pick up the phone then I need to
//remember that when I open the email I can open it again and same goes for the email window
