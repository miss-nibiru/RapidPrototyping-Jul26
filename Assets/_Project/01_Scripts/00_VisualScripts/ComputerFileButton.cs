using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerFileButton : MonoBehaviour
{
    [Header("File Data")]
    [SerializeField] private ComputerFilesData fileData;

    [Header("Optional Visuals")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI fileNameText;

    public void Setup(ComputerFilesData data)
    {
        fileData = data;

        if (fileData == null)
            return;

        if (fileNameText != null)
            fileNameText.text = fileData.fileName;

        if (iconImage != null && fileData.fileIcon != null)
            iconImage.sprite = fileData.fileIcon;
    }

    public void OpenFile()
    {
        if (fileData == null)
        {
            Debug.LogWarning("No file data assigned to this file button.");
            return;
        }

        if (ComputerWindowSpawner.Instance == null)
        {
            Debug.LogWarning("No ComputerWindowSpawner found in the scene.");
            return;
        }

        ComputerWindowSpawner.Instance.OpenFile(fileData);
    }
}