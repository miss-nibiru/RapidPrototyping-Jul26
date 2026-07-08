using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// manages only the specific functionality for each individual button type
    /// </summary>
    [SerializeField] private GameObject expandButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject sendButton;
    [SerializeField] private GameObject answerButton;
    [SerializeField] private GameObject hangUpButton;
    
    public void OnexpandButtonClicked()
    {
        //logic for when the expand button is clicked
    }
    
    public void OnSendButtonClicked()
    {
        //logic for when the send button is clicked
    }
    
    public void OnAnswerButtonClicked()
    {
        //logic for when the answer button is clicked
    }
    
    public void OnHangUpButtonClicked()
    {
        //logic for when the hangup button is clicked
    }
}
