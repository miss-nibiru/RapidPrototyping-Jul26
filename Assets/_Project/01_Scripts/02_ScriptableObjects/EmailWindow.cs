using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;
using TMPro;

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
        // Check if both slots are filled
        if (!SlotManager.Instance.AreSlotsFilled())
        {
            return;
        }

        // Get the two words from slots
        WordObject[] words = SlotManager.Instance.GetSelectedWords();
        
        // Check if both words match the correct response type
        bool bothCorrect =
            words[0].responceType == correctEmailResponceType &&
            words[1].responceType == correctEmailResponceType;

        if (bothCorrect)
        {
            GameManager.Instance.OnEmailCorrect();
        }
        else
        {
            GameManager.Instance.OnEmailIncorrect();
        }
        CloseWindow();
    }
    
}