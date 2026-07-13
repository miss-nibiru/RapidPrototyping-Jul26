
using _Project._01_Scripts._00_VisualScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class WordObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI responceText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image backgroundImage;
    
    [Header("Placement Settings")]
    [SerializeField] private float placementDistance = 100f;
    
    private Vector3 _originalPosition;
    private Vector3 _originalScale;
    private Transform _originalParent;
    private RectTransform _dragParent;
    
    public ResponceType responceType;
    public WordOptions wordOption;
    public bool placed;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(!canvas) canvas = GetComponentInParent<Canvas>();
        _dragParent = canvas.transform as RectTransform;
        _originalParent = rectTransform.parent;
        _originalPosition = rectTransform.anchoredPosition;
        _originalScale = rectTransform.localScale;
        AssignResponce(wordOption);
    }

    public void AssignResponce(WordOptions newWordOption)
    {
        wordOption = newWordOption;
        if (wordOption == null) return;

        responceText.text = wordOption.responce;
        responceText.color = wordOption.responceColor;
        if (backgroundImage != null && wordOption.responceBackground != null) backgroundImage.sprite = wordOption.responceBackground;
        
        responceType = wordOption.responceType;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (placed)
        {
            SlotManager.Instance.RemoveWordFromSlot(this);
            placed = false;
        }

        rectTransform.SetParent(_dragParent, true);
        rectTransform.SetAsLastSibling();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_dragParent == null)
            return;

        Camera cameraToUse = null;

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cameraToUse = canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _dragParent,
            eventData.position,
            cameraToUse,
            out Vector2 localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool wasPlaced = TryPlaceInNearbySlot();
        if (!wasPlaced)
        {
            ReturnToSpawn();
        }
        rectTransform.localScale = _originalScale;
    }
    
    private bool TryPlaceInNearbySlot()
    {
        Transform[] slots = SlotManager.Instance.GetAllSlots();
        
        foreach (Transform slot in slots)
        {
            float distance = Vector3.Distance(rectTransform.position, slot.position);
            if (distance < placementDistance)
            {
                bool success = SlotManager.Instance.TryPlaceWord(this, slot);
                if (success)
                {
                    placed = true;
                    return true;
                }
            }
        }
        
        return false;
    }
    public void ReturnToSpawn()
    {
        placed = false;
        SlotManager.Instance.RemoveWordFromSlot(this);
        rectTransform.SetParent(_originalParent);
        rectTransform.anchoredPosition = _originalPosition;
        rectTransform.localScale = _originalScale;
    }
    public void RemoveFromSlot()
    {
        placed = false;
        SlotManager.Instance.RemoveWordFromSlot(this);
        rectTransform.SetParent(_originalParent);
        rectTransform.anchoredPosition = _originalPosition;
        rectTransform.localScale = _originalScale;
    }
}