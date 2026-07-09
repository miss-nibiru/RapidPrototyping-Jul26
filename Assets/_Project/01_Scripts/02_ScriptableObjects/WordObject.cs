using System;
using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WordObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI responceText;
    [SerializeField] private Canvas canvas;

    public Transform originalSpawnPoint;
    public responceType responceType;
    public WordOptions wordOption;

    public bool placed;

    private void Awake()
    {
        AssignResponce();
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(!canvas) canvas = GetComponentInParent<Canvas>();
        originalSpawnPoint = this.transform;
    }

    private void AssignResponce()
    {
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
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!placed)
        {
           transform.localPosition = originalSpawnPoint.localPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Slots>())
        {
            placed = SlotManager.Instance.TryPlaceWord(this);

            if (!placed)
            {
                transform.SetParent(originalSpawnPoint);
                transform.localPosition = Vector3.zero;
            }
        }
    }
}