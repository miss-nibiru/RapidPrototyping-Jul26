using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject expandButton;
        [SerializeField] private GameObject exitButton;
        [SerializeField] private GameObject sendButton;
        [SerializeField] private GameObject answerButton;
        [SerializeField] private GameObject hangUpButton;

        public void OnexpandButtonClicked()
        {
            EmailController.Instance.OpenCurrentEmail();
        }

        public void OnSendButtonClicked()
        {
            EmailController.Instance.OnSendButtonClicked();
        }
    }
}