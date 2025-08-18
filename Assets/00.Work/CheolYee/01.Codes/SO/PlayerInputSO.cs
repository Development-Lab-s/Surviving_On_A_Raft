using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.CheolYee._01.Codes.SO
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/PlayerInputSO")]
    public class PlayerInputSo : ScriptableObject, Controls.IPlayerActions
    {
        public Action OnJumpKeyPress;
        
        public Vector2 Movement {get; private set;}
        public Vector2 MousePosition {get; private set;}
        
        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed) OnJumpKeyPress?.Invoke();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            Vector2 screenPosition = context.ReadValue<Vector2>();
            MousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
        }
    }
}
