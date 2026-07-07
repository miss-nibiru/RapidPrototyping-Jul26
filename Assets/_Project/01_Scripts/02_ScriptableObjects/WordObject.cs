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

    public void OnDrag(Vector2 position)
    {
        responceText.transform.position = position;
    }

    public void OnDrop()
    {
        //dropping logic
    }

    public void ClearWordObject()
    {
        //logic for when you either win or mess up
    }
}
