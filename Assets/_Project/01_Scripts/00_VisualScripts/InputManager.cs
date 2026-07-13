using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputAction _actions;
        private WordBank _wordBank;
        private bool _pauseInputPressed;
        
        private void Awake()
        {
            _actions = new PlayerInputAction();
            _actions.Enable();
        }

        private void OnEnable()
        {
            if (_actions.Player.Move == null)
            {
                Debug.LogError("[InputManager2D] ERROR: Player action map is missing or named incorrectly.");
                return;
            }
            _actions.Player.Move.performed += OnMove;
            _actions.Player.Move.canceled += OnMove;
            _actions.Player.Click.performed += OnClick;
            _actions.Player.pause.performed += OnPausePerformed;
            _actions.Player.pause.canceled += OnPauseCanceled;
        }

        private void OnDisable()
        {
            _actions.Player.Move.performed -= OnMove;
            _actions.Player.Move.canceled -= OnMove;
            _actions.Player.Click.performed -= OnClick;
            _actions.Player.pause.performed -= OnPausePerformed;
            _actions.Player.pause.canceled -= OnPauseCanceled;
            _actions.Disable();
        }
        
        private void OnMove(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = ctx.ReadValue<Vector2>();
        }
        
        private void OnPausePerformed(InputAction.CallbackContext ctx)
        {
            {
                _pauseInputPressed = true;
                GameManager.Instance?.TogglePause();
            }
        }

        private void OnPauseCanceled(InputAction.CallbackContext ctx)
        {
            _pauseInputPressed = false;
        }

        private void OnClick(InputAction.CallbackContext ctx)
        {
            Debug.Log("selected");
        }
    }
}