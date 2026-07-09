using System;
using Unity.VisualScripting;
using UnityEngine;
public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private responceType CorrectEmailResponceType;
    
    private void FindAnswerType(WordObject wordObject)
    {
        if (wordObject.responceType == CorrectEmailResponceType)
        {
            GameManager.Instance?.OnEmailCorrect();
            // TODO: Add any additional correct response logic here (animations, etc)
        }
        else
        {
            GameManager.Instance?.OnEmailIncorrect();
            // TODO: Add any additional incorrect response logic here (animations, etc)
        }
    }
    
    private void CheckDrop()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        
        if (other.GetComponent<WordObject>())
        {
            FindAnswerType(other.GetComponent<WordObject>());
        }
    }
}
