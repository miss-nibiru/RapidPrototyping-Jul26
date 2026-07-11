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

    [Header("Browser Button Behavior")]
    public bool browserButtonTriggersWaitingCursor = true;
    public float browserWaitingDuration = 2f;
    public bool browserButtonStartsPhoneCall;
    
    
    [Header("Error Content")]
    public string errorTitle;

    [TextArea(2, 12)]
    public string errorMessage;
    public string errorButtonText = "OK";

    [Header("Notification Content")]
    public string notificationTitle;

    [TextArea(2, 8)]
    public string notificationMessage;

    [Header("Corrupt Behaviours")]
    public bool triggersWaitingCursor;
    public float waitingDuration = 1.5f;

    public bool spawnsCriticalError;
    public ComputerFilesData criticalErrorToSpawn;

    public bool opensMultipleWindows;
    public int extraWindowCount = 1;

    [Header("Optional Settings")]
    public bool canBeOpenedMultipleTimes = true;
    public bool autoClose;
    public float autoCloseDelay = 3f;
}