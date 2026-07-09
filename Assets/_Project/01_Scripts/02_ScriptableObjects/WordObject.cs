using System;
using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WordObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI responceText;
    [SerializeField] private Canvas canvas;

    public RectTransform originalSpawnPoint;
    public responceType responceType;
    public WordOptions wordOption;

    public bool placed;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(!canvas) canvas = GetComponentInParent<Canvas>();
        AssignResponce();
        
    }

    private void AssignResponce()
    {
        originalSpawnPoint = rectTransform;
        responceText.text = wordOption.responce;
        responceText.color = wordOption.responceColor;
        responceType = wordOption.responceType;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Optional: highlight or scale
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(placed) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!placed)
        {
            rectTransform.anchoredPosition = originalSpawnPoint.position;
            rectTransform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Slots>())
        {
            placed = SlotManager.Instance.TryPlaceWord(this);

            if (!placed)
            {
                rectTransform.anchoredPosition = originalSpawnPoint.position;
                rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}