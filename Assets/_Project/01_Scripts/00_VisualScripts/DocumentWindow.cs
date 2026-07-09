using TMPro;
using UnityEngine;

public class DocumentWindow : MonoBehaviour
{
    [Header("Document UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;

    public void Setup(ComputerFilesData data)
    {
        if (data == null) return;

        if (titleText != null)
        {
            if (!string.IsNullOrEmpty(data.windowTitle))
                titleText.text = data.windowTitle;
            else
                titleText.text = data.fileName;
        }

        if (bodyText != null)
            bodyText.text = data.documentBody;
    }
}