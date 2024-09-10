using ElectrumGames.Core.Player.Movement;
using UnityEngine.InputSystem;

namespace Core.Player.Interactions
{
    public class PlayerInteraction : IInteraction, InputActions.IInteractionsActions
    {
        private InputActions _inputActions;
        
        public bool PutInteraction { get; private set; }
        public bool PrimaryInteraction { get; private set; }
        public bool AlternativeInteraction { get; private set; }
        public bool VoiceActivation { get; private set; }

        public PlayerInteraction(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void Init()
        {
            _inputActions.Interactions.SetCallbacks(this);
            _inputActions.Interactions.Enable();
        }

        public void OnPutItem(InputAction.CallbackContext context)
        {
            PutInteraction = context.phase != InputActionPhase.Canceled;
        }

        public void OnPrimaryInteraction(InputAction.CallbackContext context)
        {
            PrimaryInteraction = context.phase != InputActionPhase.Canceled;
        }

        public void OnAlternativeInteraction(InputAction.CallbackContext context)
        {
            AlternativeInteraction = context.phase != InputActionPhase.Canceled;
        }

        public void OnVoice(InputAction.CallbackContext context)
        {
            VoiceActivation = context.phase != InputActionPhase.Canceled;
        }
    }
}