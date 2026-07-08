using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
        private PlayerInputAction _actions;
        private WordBank _wordBank; 

        private void Awake()
        {
            _actions = new PlayerInputAction();
            _actions.Enable();
        }

        private void OnEnable()
        {

            // Validate action map by checking an actual action
            if (_actions.Player.Move == null)
            {
                Debug.LogError("[InputManager2D] ERROR: Player action map is missing or named incorrectly.");
                return;
            }
            
            _actions.Player.Move.performed += OnMove;
            _actions.Player.Move.canceled += OnMove;
            _actions.Player.Click.performed += OnClick;
            _actions.Player.pause.performed += OnPause;
        }

        private void OnDisable()
        {
            _actions.Player.Move.performed -= OnMove;
            _actions.Player.Move.canceled -= OnMove;
            _actions.Player.Click.performed -= OnClick;
            _actions.Player.pause.performed -= OnPause;
            _actions.Disable();
        }
        
        private void OnMove(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = ctx.ReadValue<Vector2>();
        }
        
        private void OnPause(InputAction.CallbackContext ctx)
        {
            GameManager.Instance?.Pause();
        }

        private void OnClick(InputAction.CallbackContext ctx)
        {
            Debug.Log("selected");
        }
    }
