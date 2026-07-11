using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FolderWindow : MonoBehaviour
{
    [Header("Folder UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform fileButtonParent;
    [SerializeField] private ComputerFileButton fileButtonPrefab;

    private readonly List<GameObject> _spawnedButtons = new();

    public void Setup(ComputerFilesData folderData)
    {
        ClearFolder();

        if (folderData == null)
            return;

        if (titleText != null)
        {
            if (!string.IsNullOrEmpty(folderData.windowTitle))
                titleText.text = folderData.windowTitle;
            else
                titleText.text = folderData.fileName;
        }

        foreach (ComputerFilesData file in folderData.folderContents)
        {
            if (file == null)
                continue;

            ComputerFileButton newButton = Instantiate(fileButtonPrefab, fileButtonParent);

            newButton.Setup(file);

            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(newButton.OpenFile);
            }

            _spawnedButtons.Add(newButton.gameObject);
        }
    }

    private void ClearFolder()
    {
        foreach (GameObject button in _spawnedButtons)
        {
            if (button != null)
                Destroy(button);
        }

        _spawnedButtons.Clear();
    }
}