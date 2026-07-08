using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WindowVisualController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

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
        windowPanel.gameObject.SetActive(false);
        if (!taskbarMini) return;
        taskbarMini.SetActive(true);
    }

    private void MaximizeWindow()
    {
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

    private void CloseWindow()
    {
        windowPanel.gameObject.SetActive(false);
        if (!taskbarMini) return;
        taskbarMini.SetActive(false);
    }
    
    //the windows need to be able to be dragged like a regular computer windows. Will some of these be in the way?
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            windowPanel,
            eventData.position,
            eventData.pressEventCamera,
            out _dragOffset
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragBar) return;

        windowPanel.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       //right not it doesnt need functionalty but it will host later sfx for this i thin
    }
    
    
}
