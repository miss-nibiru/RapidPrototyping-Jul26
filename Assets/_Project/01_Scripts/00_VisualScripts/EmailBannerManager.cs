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
                // Wait before spawning next banner
                yield return new WaitForSeconds(TimeManager.Instance.GetEmailBannerSpawnTime());

                // Spawn and show
                SpawnRandomBanner();
                _bannerActive = true;
                UIManager.Instance.ShowEmailBanner(_currentBanner);

                // Manual timer so SEND can interrupt expiration
                float duration = _currentBanner.bannerDuration;
                float elapsed = 0f;

                while (elapsed < duration && _bannerActive)
                {
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                // If still active, it expired naturally
                if (_bannerActive)
                {
                    CheckBannerActive();
                }

                // If not active, SEND handled it — loop continues normally
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
            newBanner.InitializeBanner(EmailBank.Instance.Banners[randomIndex]);
            _previousBanner = _currentBanner;

            EmailController.Instance.SetCurrentBanner(newBanner);

            Debug.Log($"[EmailBannerManager] Spawned new banner from {EmailBank.Instance.Banners[randomIndex].senderName}");
        }

        public void DestroyBanner()
        {
            if (_currentBanner == null)
            {
                Debug.LogWarning("[EmailBannerManager] No banners to destroy");
                return;
            }

            Destroy(_currentBanner.gameObject);
            _currentBanner = null;
            _bannerActive = false;

            Debug.Log("[EmailBannerManager] Banner destroyed");
        }

        // Called when SEND is pressed
        public void OnBannerHandled()
        {
            _bannerActive = false;
        }

        public void CheckBannerActive()
        {
            if (_bannerActive && _currentBanner != null)
            {
                Debug.Log("[EmailBannerManager] Banner expired - applying penalty and cleanup");

                GameManager.Instance.OnEmailBannerMissed();
                TimeManager.Instance.SubtractTime(_currentBanner.timePenalty);

                EmailController.Instance.OnBannerExpired();

                DestroyBanner();
            }
        }
    }
}
