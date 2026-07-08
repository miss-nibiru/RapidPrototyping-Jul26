using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordObject : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMPro.TextMeshProUGUI responceText;
    [SerializeField] private Canvas canvas;
    
    public responceType  responceType;
    public WordOptions wordOption;
    
    private void Awake()
    {
        AssignResponce();
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void AssignResponce()
    {
        responceText.text = wordOption.responce;
        responceText.color = wordOption.responceColor;
        responceType = wordOption.responceType;
    }
    
    public void ClearWordObject()
    {
        //logic for when you either win or mess up
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
}
