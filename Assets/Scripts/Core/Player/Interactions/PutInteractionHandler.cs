using Core.Items.Inventory;
using ElectrumGames.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Core.Player.Interactions.Items;
using UnityEngine;

namespace Core.Player.Interactions
{
    public class PutInteractionHandler : IInteractionItemsManager
    {
        private const int RayCastIgnoreMask = ~(1 << 2);
        
        private readonly IInteraction _interactions;
        private readonly Camera _targetCamera;
        private readonly PlayerConfig _playerConfig;
        private readonly IInventory _inventory;
        private readonly InventoryIndexHandler _inventoryIndexHandler;
        private readonly ItemInteractionVisual _itemInteractionVisual;

        private bool _previousState;
        
        public PutInteractionHandler(IInteraction interactions, Camera targetCamera, PlayerConfig playerConfig, 
            IInventory inventory, InventoryIndexHandler inventoryIndexHandler, ItemInteractionVisual interactionVisual)
        {
            _interactions = interactions;
            _targetCamera = targetCamera;
            _playerConfig = playerConfig;
            _inventory = inventory;
            _inventoryIndexHandler = inventoryIndexHandler;
            _itemInteractionVisual = interactionVisual;
        }

        public void Simulate(float deltaTime)
        {
            if (_previousState != _interactions.PutInteraction)
            {
                _previousState = !_previousState;

                if (_previousState)
                {
                    var ray = _targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

                    if (Physics.Raycast(ray, out var hit, _playerConfig.RayCastDistance, RayCastIgnoreMask))
                    {
                        if (hit.collider.TryGetComponent<ItemInstanceBase>(out var item))
                        {
                            _inventory.TryAddItem(item, _inventoryIndexHandler.CurrentIndex);
                            _itemInteractionVisual.ForceUpdate();
                        }
                    }
                }
            }
        }
    }
}