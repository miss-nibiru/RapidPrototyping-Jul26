using UnityEngine;

public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private responceType  CorrectEmailResponceType;


    private void FindAnswerType(WordObject wordObject)
    {
        if (wordObject.responceType == CorrectEmailResponceType)
        {
            Debug.Log("correct!");
            //maybe add time or something or don't subtract time
        }
        else
        {
            Debug.Log("false!");
            //penalize time or something idk.
        }
    }
    
}
