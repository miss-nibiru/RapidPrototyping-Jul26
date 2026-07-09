using UnityEngine;

public class ComputerWindowSpawner : MonoBehaviour
{
    public static ComputerWindowSpawner Instance { get; private set; }

    [Header("Window Parent")]
    [SerializeField] private RectTransform windowParent;

    [Header("Window Prefabs")]
    [SerializeField] private GameObject documentWindowPrefab;
    [SerializeField] private GameObject photoWindowPrefab;
    [SerializeField] private GameObject folderWindowPrefab;
    [SerializeField] private GameObject browserWindowPrefab;
    [SerializeField] private GameObject criticalErrorWindowPrefab;
    [SerializeField] private GameObject notificationWindowPrefab;

    private int _openWindowCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OpenFile(ComputerFilesData fileData)
    {
        if (fileData == null) return;
        if (windowParent == null) return;

        GameObject prefabToOpen = GetPrefabForFileType(fileData.fileType);

        if (prefabToOpen == null)
        {
            Debug.LogWarning("No window prefab assigned for file type: " + fileData.fileType);
            return;
        }

        GameObject newWindow = Instantiate(prefabToOpen, windowParent);

        RectTransform windowRect = newWindow.GetComponent<RectTransform>();
        if (windowRect != null)
        {
            windowRect.anchoredPosition += new Vector2(_openWindowCount * 25f, _openWindowCount * -25f);
            windowRect.SetAsLastSibling();

            if (fileData.windowSize != Vector2.zero)
                windowRect.sizeDelta = fileData.windowSize;
        }

        DocumentWindow documentWindow = newWindow.GetComponent<DocumentWindow>();
        if (documentWindow != null)
            documentWindow.Setup(fileData);

        _openWindowCount++;
    }

    private GameObject GetPrefabForFileType(ComputerFileType fileType)
    {
        switch (fileType)
        {
            case ComputerFileType.Document:
                return documentWindowPrefab;

            case ComputerFileType.Photo:
                return photoWindowPrefab;

            case ComputerFileType.Folder:
                return folderWindowPrefab;

            case ComputerFileType.BrowserPage:
            case ComputerFileType.BrowserWindow:
                return browserWindowPrefab;

            case ComputerFileType.CriticalError:
            case ComputerFileType.CorruptFile:
                return criticalErrorWindowPrefab;

            case ComputerFileType.Notification:
                return notificationWindowPrefab;

            default:
                return null;
        }
    }
}