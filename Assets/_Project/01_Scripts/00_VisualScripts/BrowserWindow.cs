using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Project._01_Scripts._00_VisualScripts;

public class BrowserWindow : MonoBehaviour
{
    [Header("Browser UI")]
    [SerializeField] private Image browserImage;
    [SerializeField] private Button browserButton;
    [SerializeField] private TextMeshProUGUI browserButtonText;
    [SerializeField] private RectTransform browserButtonRect;

    private ComputerFilesData _currentPage;

    public void Setup(ComputerFilesData pageData)
    {
        _currentPage = pageData;

        if (_currentPage == null) return;
        if (browserImage != null && _currentPage.browserImage != null) browserImage.sprite = _currentPage.browserImage;
        if (browserButtonText != null) browserButtonText.text = _currentPage.browserButtonText;
        if (browserButton != null)
        {
            browserButton.onClick.RemoveListener(OnBrowserButtonClicked);
            browserButton.onClick.AddListener(OnBrowserButtonClicked);
        }
        if (browserButton != null) browserButton.gameObject.SetActive(_currentPage.showBrowserButton);
        if (browserButtonRect != null) 
            browserButtonRect.anchoredPosition = _currentPage.browserButtonPosition;
        
        
    }

    private void OnBrowserButtonClicked()
    {
        if (_currentPage == null) return;

        switch (_currentPage.browserButtonAction)
        {
            case BrowserButtonAction.None: break;

            case BrowserButtonAction.AddTime: TimeManager.Instance.AddTime(_currentPage.browserTimeAmount); break;

            case BrowserButtonAction.ReduceTime: TimeManager.Instance.SubtractTime(_currentPage.browserTimeAmount); break;

            case BrowserButtonAction.SpawnCriticalErrors:
                ComputerWindowSpawner.Instance.StartCriticalErrorSequence(
                    _currentPage.criticalErrorToSpawn,
                    _currentPage.criticalErrorCount,
                    _currentPage.criticalErrorSpawnInterval
                );
                break;
        }
    }
}