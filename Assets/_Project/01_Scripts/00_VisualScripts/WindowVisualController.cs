using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// this script can receive events - its mostly a helper for animation and sfx functionality
/// drags windows, minimizes, maximizes
/// Self contained, this is not needed for anything else
/// </summary>
public class WindowVisualController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    
    [SerializeField] private RectTransform windowPanel;
    
    [Header("Buttons")]
    [SerializeField] private Button minimizeButton;
    [SerializeField] private Button maximizeButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject taskbarMini;
    [SerializeField] private RectTransform dragBar;
    
    [Header("Window Size Settings")]
    [SerializeField] private float maximumWindowSize = 1.5f;    
    
    private Vector2 _normalSize;
    private Vector2 _normalPosition;
    private bool _isMaximized;
    private Vector2 _dragOffset;

    private void Awake()
    {
        _normalSize = windowPanel.sizeDelta;
        _normalPosition = windowPanel.anchoredPosition;
        
        if (!taskbarMini) return;
        taskbarMini.SetActive(false);
        
    }

    private void OnEnable()
    {
        minimizeButton.onClick.AddListener(MinimizeWindow);
        maximizeButton.onClick.AddListener(MaximizeWindow);
        closeButton.onClick.AddListener(CloseWindow);
        
    }

    private void OnDisable()
    {
        minimizeButton.onClick.RemoveListener(MinimizeWindow);
        maximizeButton.onClick.RemoveListener(MaximizeWindow);
        closeButton.onClick.RemoveListener(CloseWindow);
    }

    private void MinimizeWindow()
    {
        BringToFront();
        windowPanel.gameObject.SetActive(false);
        if (!taskbarMini) return;
        taskbarMini.SetActive(true);
    }

    private void MaximizeWindow()
    {
        BringToFront();
        if (!_isMaximized)
        {
            _normalSize = windowPanel.sizeDelta;
            _normalPosition = windowPanel.anchoredPosition;

            windowPanel.localScale = new Vector3(maximumWindowSize, maximumWindowSize, 1f);

            _isMaximized = true;
        }
        else
        {
            windowPanel.localScale = Vector3.one;
            windowPanel.sizeDelta = _normalSize;
            windowPanel.anchoredPosition = _normalPosition;

            _isMaximized = false;
        }
    }
    
    public void OpenWindow()
    {
        windowPanel.gameObject.SetActive(true);
        BringToFront();
    }

    private void CloseWindow()
    {
        windowPanel.gameObject.SetActive(false);
        if (!taskbarMini) return;
        taskbarMini.SetActive(false);
    }
    
    //the windows need to be able to be dragged like a regular computer windows. Will some of these be in the way?
    
    private void BringToFront()
    {
        windowPanel.SetAsLastSibling();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        BringToFront();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(windowPanel, eventData.position, eventData.pressEventCamera, out _dragOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragBar) return;

        windowPanel.SetAsLastSibling();
        windowPanel.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       //right now it doesnt need functionalty but it will host later sfx for this i think
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BringToFront();
    }
    
}
