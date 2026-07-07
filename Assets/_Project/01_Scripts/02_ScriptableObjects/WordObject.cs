using System;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI responceText;
    
    public responceType  responceType;
    public WordOptions wordOption;
    
    private void Awake()
    {
        AssignResponce();
    }
    
    private void AssignResponce()
    {
        responceText.text = wordOption.responce;
        responceText.color = wordOption.responceColor;
        responceType = wordOption.responceType;
    }
}
