using UnityEngine;

public class BrowserManager : MonoBehaviour
{
    public static BrowserManager Instance { get; private set; }

    [SerializeField] private BrowserOptions browserBank;

    private ComputerFilesData _lastPage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OpenRandomBrowserPage()
    {
        Debug.Log("BROWSER ICON CLICKED");

        if (browserBank == null)
            return;

        if (browserBank.browserPages.Count == 0)
            return;

        ComputerFilesData randomPage;

        do
        {
            randomPage = browserBank.browserPages[
                Random.Range(0, browserBank.browserPages.Count)];
        }
        while (browserBank.browserPages.Count > 1 &&
               randomPage == _lastPage);

        _lastPage = randomPage;

        Debug.Log("Opening browser page: " + randomPage.fileName);

        ComputerWindowSpawner.Instance.OpenFile(randomPage);
    }
    
}