using Core.Items.Inventory;
using ElectrumGames.Core.Items.Inventory;
using UnityEngine;

namespace Core.Player.Interactions
{
    public class DropInteractionHandler
    {
        private readonly IInteraction _interactions;
        private readonly Transform _outputTransform;
        private readonly IInventory _inventory;
        private readonly InventoryIndexHandler _inventoryIndexHandler;

        private bool _previousState;
        
        public DropInteractionHandler(IInteraction interactions, Transform outputTransform, IInventory inventory, InventoryIndexHandler inventoryIndexHandler)
        {
            _interactions = interactions;
            _outputTransform = outputTransform;
            _inventory = inventory;
            _inventoryIndexHandler = inventoryIndexHandler;
        }

        public void Simulate(float deltaTime)
        {
            if (_previousState != _interactions.DropItem)
            {
                _previousState = !_previousState;

                if (_previousState)
                {
                    _inventory.Items[_inventoryIndexHandler.CurrentIndex].OnDropItem(_outputTransform);
                    _inventory.Items[_inventoryIndexHandler.CurrentIndex] = null;
                }
            }
        }
    }
}