using System.Collections.Generic;
using UnityEngine;

public enum ComputerFileType
{
    Document,
    Photo,
    Folder,
    BrowserPage,
    BrowserWindow,
    CriticalError,
    CorruptFile,
    Notification,
}

public enum BrowserButtonAction
{
    None,
    AddTime,
    ReduceTime,
    SpawnCriticalErrors
}

public enum CriticalErrorButtonAction
{
    None,
    AddTime,
    ReduceTime
}

[CreateAssetMenu(fileName = "NewComputerFilesData", menuName = "Scriptable Objects/Computer Files Data")]
public class ComputerFilesData : ScriptableObject
{
    
    [Header("Basic File Info")]
    public string fileName;
    public Sprite fileIcon;
    public ComputerFileType fileType;

    [Header("Window Info")]
    public string windowTitle;
    public Sprite windowIcon;
    public Vector2 windowSize = new Vector2(800, 600);

    [Header("Document Content")]
    [TextArea(5, 30)]
    public string documentBody;

    [Header("Photo Content")]
    public Sprite photoImage;

    [Header("Folder Content")]
    public List<ComputerFilesData> folderContents = new();

    [Header("Browser Content")]
    public Sprite browserImage;
    public string browserButtonText = "Buy Now";
    public bool showBrowserButton = true;
    public Vector2 browserButtonPosition;

    [Header("Browser Button Behavior")]
    public BrowserButtonAction browserButtonAction = BrowserButtonAction.None;
    public float browserTimeAmount = 5f;

    public ComputerFilesData criticalErrorToSpawn;
    public int criticalErrorCount = 5;
    public float criticalErrorSpawnInterval = 1f;
    
    [Header("Error Content")]
    public string errorTitle;

    [TextArea(2, 12)]
    public string errorMessage;
    public string errorButtonText = "OK";
    public CriticalErrorButtonAction criticalErrorButtonAction = CriticalErrorButtonAction.None;

    public float criticalErrorTimeAmount = 5f;

    [Header("Notification Content")]
    public string notificationTitle;
    [TextArea(2, 8)]
    public string notificationMessage;
    
    [Header("Optional Settings")]
    public bool canBeOpenedMultipleTimes = true;
    public bool autoClose;
    public float autoCloseDelay = 3f;
}