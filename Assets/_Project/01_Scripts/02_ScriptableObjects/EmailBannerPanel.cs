using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;

namespace _Project._01_Scripts._02_ScriptableObjects
{
    public class EmailBannerPanel : MonoBehaviour
    {
        [SerializeField] private GameObject bannerPanel;

        public void OpenEmailWindow()
        {
            bannerPanel.SetActive(false);
            GameManager.Instance.OnEmailBannerOpened();
        }
    }
}

