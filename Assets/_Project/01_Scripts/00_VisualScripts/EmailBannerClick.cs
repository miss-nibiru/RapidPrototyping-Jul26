using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;
using UnityEngine.UI;

public class EmailBannerClick : MonoBehaviour
{
    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnBannerClicked);
        }
    }

    private void OnBannerClicked()
    {
        EmailController.Instance.OpenCurrentEmail();
    }
}