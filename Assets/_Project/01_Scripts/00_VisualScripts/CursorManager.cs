using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public bool IsWaiting => _isWaiting;

    [Header("Cursors")]
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Texture2D waitingCursor;

    [Header("Cursor Hotspots")]
    [SerializeField] private Vector2 normalHotspot = Vector2.zero;
    [SerializeField] private Vector2 hoverHotspot = Vector2.zero;
    [SerializeField] private Vector2 waitingHotspot = Vector2.zero;

    [Header("Random Waiting")]
    [SerializeField] private bool useRandomWaiting = true;
    [SerializeField] private float minSecondsBetweenWaits = 12f;
    [SerializeField] private float maxSecondsBetweenWaits = 25f;
    [SerializeField] private float minWaitDuration = 1f;
    [SerializeField] private float maxWaitDuration = 2f;

    private bool _isWaiting;
    private Coroutine _waitingRoutine;
    private Coroutine _randomWaitingRoutine;

    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;
    private readonly List<RaycastResult> _raycastResults = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _eventSystem = EventSystem.current;

        SetNormalCursor();

        if (useRandomWaiting)
            _randomWaitingRoutine = StartCoroutine(RandomWaitingLoop());
    }

    private void Update()
    {
        if (_isWaiting) return;

        if (IsPointerOverButton())
            SetHoverCursor();
        else
            SetNormalCursor();
    }

    public void SetNormalCursor()
    {
        if (_isWaiting) return;

        Cursor.SetCursor(normalCursor, normalHotspot, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        if (_isWaiting) return;

        Cursor.SetCursor(hoverCursor, hoverHotspot, CursorMode.Auto);
    }

    private void SetWaitingCursor()
    {
        Cursor.SetCursor(waitingCursor, waitingHotspot, CursorMode.Auto);
    }

    public void StartWaiting(float seconds)
    {
        if (_waitingRoutine != null)
        {
            StopCoroutine(_waitingRoutine);
            SetUIInputBlocked(false);
        }

        _waitingRoutine = StartCoroutine(WaitingRoutine(seconds));
    }

    private IEnumerator WaitingRoutine(float seconds)
    {
        _isWaiting = true;

        SetWaitingCursor();
        SetUIInputBlocked(true);

        yield return new WaitForSeconds(seconds);

        SetUIInputBlocked(false);

        _isWaiting = false;
        _waitingRoutine = null;

        SetNormalCursor();
    }

    private IEnumerator RandomWaitingLoop()
    {
        while (true)
        {
            float timeUntilNextWait = Random.Range(minSecondsBetweenWaits, maxSecondsBetweenWaits);
            yield return new WaitForSeconds(timeUntilNextWait);

            float waitDuration = Random.Range(minWaitDuration, maxWaitDuration);
            StartWaiting(waitDuration);

            yield return new WaitForSeconds(waitDuration);
        }
    }

    private void SetUIInputBlocked(bool blocked)
    {
        if (_eventSystem != null)
            _eventSystem.enabled = !blocked;
    }

    private bool IsPointerOverButton()
    {
        if (_eventSystem == null || Mouse.current == null)
            return false;

        _pointerEventData ??= new PointerEventData(_eventSystem);
        _pointerEventData.position = Mouse.current.position.ReadValue();

        _raycastResults.Clear();
        _eventSystem.RaycastAll(_pointerEventData, _raycastResults);

        foreach (RaycastResult result in _raycastResults)
        {
            Button button = result.gameObject.GetComponentInParent<Button>();

            if (button != null && button.interactable)
                return true;
        }

        return false;
    }
}