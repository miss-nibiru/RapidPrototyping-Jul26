using System.Collections;
using System.Collections.Generic;
using _Project._01_Scripts._02_ScriptableObjects;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class EmailBannerManager : MonoBehaviour
    {
        public static EmailBannerManager Instance { get; private set; }

        [Header("banner Types")]
        [SerializeField] private List<EmailBannerSO> emailBanners = new();
        
        private Coroutine _bannerRoutine;
        private bool _bannerActive;
        private EmailBannerPanel _currentBanner;
        private EmailBannerPanel _previousBanner;
        private bool _spawningBanner = true;

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
            while (_spawningBanner)
            {
                yield return new WaitForSeconds(TimeManager.Instance.GetEmailBannerSpawnTime());
                SpawnRandomBanner();
                _bannerActive = true;
                UIManager.Instance.ShowEmailBanner(_currentBanner);
                Invoke(nameof(CheckBannerActive), _currentBanner.bannerDuration);
            }
        }
        
        public void SpawnRandomBanner()
        {
            var randomIndex = Random.Range(0, EmailBank.Instance.Banners.Count);
            var newBanner = Instantiate(EmailBank.Instance.BannerPrefabRef, EmailBank.Instance.SpawnPoint);
            EmailBank.Instance.SpawnedBanners.Enqueue(newBanner.gameObject);
            newBanner.gameObject.transform.SetParent(EmailBank.Instance.SpawnPoint);
            UIManager.Instance.EmailBannerUI = newBanner.gameObject;
            _currentBanner = newBanner;
            newBanner.Initialize(EmailBank.Instance.Banners[randomIndex]);
            _previousBanner = _currentBanner;
        }

        public void DestroyBanner()
        {
            var last = EmailBank.Instance.SpawnedBanners.Dequeue();
            Destroy(last.gameObject);
        }

        public void CheckBannerActive()
        {
            if (_bannerActive)
            {
                GameManager.Instance.OnEmailBannerMissed();
                TimeManager.Instance.SubtractTime(_currentBanner.timePenalty);
                DestroyBanner();
            }
        }
    }
}

//I have a possible edge case now because if I can be able to bring back up the email again if I want to pick up the phone then I need to
//remember that when I open the email I can open it again and same goes for the email window
