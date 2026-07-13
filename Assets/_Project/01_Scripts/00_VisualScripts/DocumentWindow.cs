using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Project._01_Scripts._00_VisualScripts;

public class DocumentWindow : MonoBehaviour
{
    [Header("Document UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;

    [Header("Optional Button")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private ComputerFilesData currentData;

    public void Setup(ComputerFilesData data)
    {
        if (data == null)
        {
            Debug.LogWarning("DocumentWindow received no ComputerFilesData.");
            return;
        }

        currentData = data;

        SetupDocumentText();
        SetupButton();
    }

    private void SetupDocumentText()
    {
        if (titleText != null)
        {
            titleText.text = !string.IsNullOrEmpty(currentData.windowTitle)
                ? currentData.windowTitle
                : currentData.fileName;
        }

        if (bodyText != null)
        {
            bodyText.text = currentData.documentBody;
        }
    }

    private void SetupButton()
    {
        if (button == null)
        {
            Debug.LogWarning("Document button is not assigned.");
            return;
        }

        button.gameObject.SetActive(currentData.hasButton);

        if (!currentData.hasButton)
        {
            return;
        }

        if (buttonText != null)
        {
            buttonText.text = currentData.buttonText;
        }
    }

    public void OnButtonClick()
    {
        if (currentData == null)
        {
            Debug.LogWarning("DocumentWindow has no current file data.");
            return;
        }

        switch (currentData.buttonAction)
        {
            case DocumentButtonAction.None:
                break;

            case DocumentButtonAction.AddTime:
                TimeManager.Instance.AddTime(currentData.timeAmount);
                break;

            case DocumentButtonAction.ReduceTime:
                TimeManager.Instance.SubtractTime(currentData.timeAmount);
                break;

            case DocumentButtonAction.WinGame:
                GameManager.Instance.WinGame();
                break;
        }
    }
}