using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [Header("Score Thresholds")]
        [SerializeField] private float highScoreThreshold = 90f;
        [SerializeField] private float mediumScoreThreshold = 60f;
        [SerializeField] private float lowScoreThreshold = 30f;

        [Header("Score Labels")]
        [SerializeField] private string highScoreLabel = "EXCELLENT!";
        [SerializeField] private string mediumScoreLabel = "GOOD!";
        [SerializeField] private string lowScoreLabel = "OKAY";

        public enum ScoreCategory { High, Medium, Low, None }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public string GetScoreCategoryLabel(float elapsedTime)
        {
            if (elapsedTime >= highScoreThreshold) return highScoreLabel;
            if (elapsedTime >= mediumScoreThreshold) return mediumScoreLabel;
            if (elapsedTime >= lowScoreThreshold) return lowScoreLabel;
            return "NO SCORE";
        }
    }
}