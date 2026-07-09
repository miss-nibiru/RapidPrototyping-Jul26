using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;

public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private responceType correctEmailResponceType;

    public void OpenWindow()
    {
        emailPanel.SetActive(true);
        WordBank.Instance.SpawnWords();
    }

    public void CloseWindow()
    {
        emailPanel.SetActive(false);
        WordBank.Instance.ClearWords();
        SlotManager.Instance.ClearSlots();
    }

    public void OnSendButtonClicked()
    {
        if (!SlotManager.Instance.AreSlotsFilled())
        {
            Debug.Log("EmailWindow: Need two words before sending.");
            return;
        }

        WordObject[] words = SlotManager.Instance.GetSelectedWords();

        bool bothCorrect =
            words[0].responceType == correctEmailResponceType &&
            words[1].responceType == correctEmailResponceType;

        if (bothCorrect)
            GameManager.Instance.OnEmailCorrect();
        else
            GameManager.Instance.OnEmailIncorrect();

        CloseWindow();
    }

}