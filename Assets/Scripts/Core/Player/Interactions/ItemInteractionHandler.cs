using Core.Items.Inventory;
using ElectrumGames.Core.Items.Inventory;

namespace Core.Player.Interactions
{
    public class ItemInteractionHandler : IInteractionItemsManager
    {
        private readonly IInteraction _interactions;
        private readonly IInventory _inventory;
        private readonly InventoryIndexHandler _inventoryIndexHandler;

        private bool _lastPrimaryInteractionState;
        private bool _lastAlternativeInteractionState;

        public ItemInteractionHandler(IInteraction interactions, IInventory inventory, InventoryIndexHandler inventoryIndexHandler)
        {
            _interactions = interactions;
            _inventory = inventory;
            _inventoryIndexHandler = inventoryIndexHandler;
        }

        public void Simulate(float deltaTime)
        {
            if (_lastPrimaryInteractionState != _interactions.PrimaryInteraction)
            {
                _lastPrimaryInteractionState = !_lastPrimaryInteractionState;

                if (_lastPrimaryInteractionState && _inventory.Items[_inventoryIndexHandler.CurrentIndex] != null)
                {
                    _inventory.Items[_inventoryIndexHandler.CurrentIndex].OnMainInteraction();
                }
            }

            if (_lastAlternativeInteractionState != _interactions.AlternativeInteraction)
            {
                _lastAlternativeInteractionState = !_lastAlternativeInteractionState;
                
                if (_lastAlternativeInteractionState && _inventory.Items[_inventoryIndexHandler.CurrentIndex] != null)
                {
                    _inventory.Items[_inventoryIndexHandler.CurrentIndex].OnAlternativeInteraction();
                }
            }
        }
    }
}