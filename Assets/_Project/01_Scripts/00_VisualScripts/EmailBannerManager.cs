using System.Collections;
using System.Collections.Generic;
using _Project._01_Scripts._02_ScriptableObjects;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class EmailBannerManager : MonoBehaviour
    {
        public static EmailBannerManager Instance { get; private set; }

        private Coroutine _bannerSpawnRoutine;
        private bool _spawningBanner = true;
        private Queue<EmailBannerPanel> _activeBanners = new Queue<EmailBannerPanel>();
        private Dictionary<EmailBannerPanel, Coroutine> _bannerExpirationCoroutines =
            new Dictionary<EmailBannerPanel, Coroutine>();
        private EmailBannerPanel _currentBanner;
        
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
            if (_bannerSpawnRoutine != null)
                StopCoroutine(_bannerSpawnRoutine);

            _bannerSpawnRoutine = StartCoroutine(BannerSpawnLoopRoutine());
        }

        private IEnumerator BannerSpawnLoopRoutine()
        {
            while (_spawningBanner)
            {
                yield return new WaitForSeconds(TimeManager.Instance.GetEmailBannerSpawnTime());
                SpawnRandomBanner();
            }
        }

        public void SpawnRandomBanner()
        {
            if (EmailBank.Instance.Banners.Count == 0)
            {
                return;
            }

            var randomIndex = Random.Range(0, EmailBank.Instance.Banners.Count);
            var newBanner = Instantiate(
                EmailBank.Instance.BannerPrefabRef,
                EmailBank.Instance.SpawnPoint
            );

            
            newBanner.InitializeBanner(EmailBank.Instance.Banners[randomIndex]);
            _activeBanners.Enqueue(newBanner);
            _currentBanner = newBanner;
            EmailBank.Instance.SpawnedBanners.Enqueue(newBanner.gameObject);
            StartBannerExpirationTimer(newBanner);
            EmailController.Instance.SetCurrentBanner(newBanner);
            
        }

        private void StartBannerExpirationTimer(EmailBannerPanel banner)
        {
            if (_bannerExpirationCoroutines.ContainsKey(banner)) StopCoroutine(_bannerExpirationCoroutines[banner]);
            
            Coroutine expirationRoutine = StartCoroutine(BannerExpirationRoutine(banner));
            _bannerExpirationCoroutines[banner] = expirationRoutine;
        }

        private IEnumerator BannerExpirationRoutine(EmailBannerPanel banner)
        {
            float duration = banner.bannerDuration;
            yield return new WaitForSeconds(duration);

            if (_activeBanners.Contains(banner))
            {
                if (banner.emailBannerSo.penalizesTimeWhenMissed)
                {
                    TimeManager.Instance.SubtractTime(banner.timePenalty);
                    UIManager.Instance.ShowPenaltyText("EMAIL MISSED", Color.white);
                }

                if (EmailController.Instance.GetCurrentBannerPanel() == banner)
                {
                    EmailController.Instance.OnBannerExpired();
                }
                
                DestroyBanner(banner, true, false);
            }
            
            if (_bannerExpirationCoroutines.ContainsKey(banner)) _bannerExpirationCoroutines.Remove(banner);
        }

        public void DestroyBanner(EmailBannerPanel bannerToDestroy = null, bool playFeedback = false, bool wasSuccessful = false)
        {
            if (bannerToDestroy == null)
            {
                if (_activeBanners.Count == 0)
                {
                    return;
                }
                bannerToDestroy = _activeBanners.Dequeue();
            }
            else
            {
                Queue<EmailBannerPanel> tempQueue = new Queue<EmailBannerPanel>();
                while (_activeBanners.Count > 0)
                {
                    
                    EmailBannerPanel banner = _activeBanners.Dequeue();
                    if (banner != bannerToDestroy) tempQueue.Enqueue(banner);
                    
                    
                }
                _activeBanners = tempQueue;
            }
            if (_bannerExpirationCoroutines.ContainsKey(bannerToDestroy))
            {
                StopCoroutine(_bannerExpirationCoroutines[bannerToDestroy]);
                _bannerExpirationCoroutines.Remove(bannerToDestroy);
            }

            if (bannerToDestroy != null)
            {
                if (playFeedback)
                {
                    if (wasSuccessful) bannerToDestroy.PlaySuccessFeedbackThenDestroy();
                    else bannerToDestroy.PlayFailFeedbackThenDestroy();
                }
                else
                {
                    bannerToDestroy.gameObject.SetActive(false);
                    Destroy(bannerToDestroy.gameObject);
                }
            }
        }

        public void OnBannerHandled(EmailBannerPanel handledBanner, bool wasSuccessful)
        {
            if (handledBanner == null)
            {
                return;
            }

            DestroyBanner(handledBanner, true, wasSuccessful);
        }

        public EmailBannerPanel GetCurrentBanner()
        {
            return _currentBanner;
        }

        public Queue<EmailBannerPanel> GetActiveBanners()
        {
            return new Queue<EmailBannerPanel>(_activeBanners);
        }

        public int GetActiveBannerCount()
        {
            return _activeBanners.Count;
        }

        public void StopSpawning()
        {
            _spawningBanner = false;
            if (_bannerSpawnRoutine != null)
            {
                StopCoroutine(_bannerSpawnRoutine);
                _bannerSpawnRoutine = null;
            }
        }

        public void ClearAllBanners()
        {
            foreach (var banner in _activeBanners)
            {
                if (_bannerExpirationCoroutines.ContainsKey(banner))
                {
                    StopCoroutine(_bannerExpirationCoroutines[banner]);
                    _bannerExpirationCoroutines.Remove(banner);
                }
                if (banner != null)
                {
                    Destroy(banner.gameObject);
                }
            }
            _activeBanners.Clear();
            _bannerExpirationCoroutines.Clear();
            _currentBanner = null;
           
        }
    }
}
