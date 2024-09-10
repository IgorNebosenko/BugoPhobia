using UnityEngine;
using UnityEngine.InputSystem;

namespace ElectrumGames.Core.Player.Movement
{
    public class PlayerInput : IInput, InputActions.IMovingActions
    {
        private InputActions _inputActions;

        private bool _isMovementUpdated;
        private bool _isLookUpdated;
        
        public Vector2 Movement { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Sprint { get; private set; }
        public bool IsCrouch { get; private set; }

        public PlayerInput(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void Init()
        {
            _inputActions.Moving.SetCallbacks(this);
            _inputActions.Moving.Enable();
        }

        public void Update(float deltaTime)
        {
            Movement = _isMovementUpdated ? _inputActions.Moving.Move.ReadValue<Vector2>() : Vector2.zero;
            Look = _isLookUpdated ? _inputActions.Moving.Look.ReadValue<Vector2>() : Vector2.zero;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _isMovementUpdated = context.phase != InputActionPhase.Canceled;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _isLookUpdated = context.phase != InputActionPhase.Canceled;
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            Sprint = context.phase != InputActionPhase.Canceled;
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            IsCrouch = context.phase == InputActionPhase.Performed;
        }
    }
}