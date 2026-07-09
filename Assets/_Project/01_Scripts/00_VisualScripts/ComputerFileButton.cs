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

        if (fileData == null) return;

        if (iconImage != null)
            iconImage.sprite = fileData.fileIcon;

        if (fileNameText != null)
            fileNameText.text = fileData.fileName;
    }

    public void OpenFile()
    {
        if (fileData == null)
        {
            Debug.LogWarning("ComputerFileButton has no file data assigned.");
            return;
        }

        if (ComputerWindowSpawner.Instance == null)
        {
            Debug.LogWarning("No ComputerWindowSpawner found in scene.");
            return;
        }

        ComputerWindowSpawner.Instance.OpenFile(fileData);
    }
}