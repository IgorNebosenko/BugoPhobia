using Core.Items.Inventory;
using ElectrumGames.Core.Player.Movement;
using UnityEngine.InputSystem;

namespace Core.Player.Interactions
{
    public class PlayerInteraction : IInteraction, InputActions.IInteractionsActions
    {
        private InputActions _inputActions;
        private InventoryIndexHandler _inventoryIndexHandler;
        
        public bool PutInteraction { get; private set; }
        public bool PrimaryInteraction { get; private set; }
        public bool AlternativeInteraction { get; private set; }
        public bool VoiceActivation { get; private set; }
        public bool FirstSlotSelected { get; private set; }
        public bool SecondSlotSelected { get; private set; }
        public bool ThirdSlotSelected { get; private set; }
        public bool FourthSlotSelected { get; private set; }
        public bool NextSlotSelected { get; private set; }
        public bool DropItem { get; private set; }

        public PlayerInteraction(InputActions inputActions, InventoryIndexHandler inventoryIndexHandler)
        {
            _inputActions = inputActions;
            _inventoryIndexHandler = inventoryIndexHandler;
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

        public void OnFirstSlotSelected(InputAction.CallbackContext context)
        {
            FirstSlotSelected = context.phase != InputActionPhase.Canceled;
        }

        public void OnSecondSlotSelected(InputAction.CallbackContext context)
        {
            SecondSlotSelected = context.phase != InputActionPhase.Canceled;
        }

        public void OnThirdSlotSelected(InputAction.CallbackContext context)
        {
            ThirdSlotSelected = context.phase != InputActionPhase.Canceled;
        }

        public void OnFourthSlotSelected(InputAction.CallbackContext context)
        {
            FourthSlotSelected = context.phase != InputActionPhase.Canceled;
        }

        public void OnNextSlotInventory(InputAction.CallbackContext context)
        {
            NextSlotSelected = context.phase != InputActionPhase.Canceled;
        }

        public void OnDropItem(InputAction.CallbackContext context)
        {
            DropItem = context.phase != InputActionPhase.Canceled;
        }
    }
}