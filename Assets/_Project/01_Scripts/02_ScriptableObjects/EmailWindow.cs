using UnityEngine;

public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private responceType CorrectEmailResponceType;

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
            words[0].responceType == CorrectEmailResponceType &&
            words[1].responceType == CorrectEmailResponceType;

        if (bothCorrect)
            GameManager.Instance.OnEmailCorrect();
        else
            GameManager.Instance.OnEmailIncorrect();

        CloseWindow();
    }

}