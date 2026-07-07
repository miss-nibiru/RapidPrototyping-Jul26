using UnityEngine;

public class PhoneWindow : MonoBehaviour
{
    
    [SerializeField] private GameObject phonePanel;
    [SerializeField] private responceType  correctPhoneResponceType;
    
    private void FindAnswerType(WordObject wordObject)
    {
        if (wordObject.responceType == correctPhoneResponceType)
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
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
