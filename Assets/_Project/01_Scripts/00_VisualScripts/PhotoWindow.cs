using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoWindow : MonoBehaviour
{
    [Header("Photo UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image photoImage;

    public void Setup(ComputerFilesData data)
    {
        if (data == null) return;
        if (titleText != null) titleText.text = !string.IsNullOrEmpty(data.windowTitle) ? data.windowTitle : data.fileName;
        if (photoImage != null) photoImage.sprite = data.photoImage;
    }
}