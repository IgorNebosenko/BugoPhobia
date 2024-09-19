using ElectrumGames.Core.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ElectrumGames.Core.Boards
{
    public class BoardInputHandler : MonoBehaviour, InputActions.IBoardsActions, IBoardInput
    {
        public bool Return { get; private set; }
        public bool Open { get; private set; }
        public bool MoveRight { get; private set; }
        public bool MoveLeft { get; private set; }

        private InputActions _inputActions;
        
        [Inject]
        private void Construct(InputActions inputActions)
        {
            _inputActions = inputActions;
            
            _inputActions.Boards.SetCallbacks(this);
            _inputActions.Boards.Enable();
        }

        public void OnReturn(InputAction.CallbackContext context)
        {
            Return = context.phase != InputActionPhase.Canceled;
        }

        public void OnOpen(InputAction.CallbackContext context)
        {
            Open = context.phase != InputActionPhase.Canceled;
        }

        public void OnMoveRight(InputAction.CallbackContext context)
        {
            MoveRight = context.phase != InputActionPhase.Canceled;
        }

        public void OnMoveLeft(InputAction.CallbackContext context)
        {
            MoveLeft = context.phase != InputActionPhase.Canceled;
        }
    }
}