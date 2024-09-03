using ElectrumGames.Core.Player.Movement;
using UnityEngine.InputSystem;

namespace ElectrumGames.UI.UiEvents
{
    public class UiEventInput : IUiEventInput, InputActions.IUiEventsActions
    {
        private readonly InputActions _inputActions;
        
        public bool IsJournalPressed { get; private set; }
        public bool IsMenuPressed { get; private set; }

        public UiEventInput(InputActions inputActions)
        {
            _inputActions = inputActions;
            
            _inputActions.UiEvents.SetCallbacks(this);
            _inputActions.UiEvents.Enable();
        }

        public void OnDestroy()
        {
            _inputActions.UiEvents.Disable();
        }
        
        public void OnOpenJournal(InputAction.CallbackContext context)
        {
            IsJournalPressed = context.phase != InputActionPhase.Canceled;
        }

        public void OnOpenMenu(InputAction.CallbackContext context)
        {
            IsMenuPressed = context.phase != InputActionPhase.Canceled;
        }
    }
}