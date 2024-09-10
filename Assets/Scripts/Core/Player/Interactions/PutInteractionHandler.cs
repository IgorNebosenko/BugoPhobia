using ElectrumGames.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using UnityEngine;

namespace Core.Player.Interactions
{
    public class PutInteractionHandler
    {
        private readonly IInteraction _interactions;
        private readonly Camera _targetCamera;
        private readonly PlayerConfig _playerConfig;
        private readonly IInventory _inventory;

        private bool _previousState;
        
        public PutInteractionHandler(IInteraction interactions, Camera targetCamera, PlayerConfig playerConfig, 
            IInventory inventory)
        {
            _interactions = interactions;
            _targetCamera = targetCamera;
            _playerConfig = playerConfig;
            _inventory = inventory;
        }

        public void Simulate(float deltaTime)
        {
            if (_previousState != _interactions.PutInteraction)
            {
                _previousState = !_previousState;

                if (_previousState)
                {
                    var ray = _targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

                    if (Physics.Raycast(ray, out var hit, _playerConfig.RayCastDistance))
                    {
                        if (hit.collider.TryGetComponent<ItemInstanceBase>(out var item))
                        {
                            _inventory.TryAddItem(item, 0);
                            Debug.Log($"Hit into {item.ItemType}");
                        }
                    }
                }
            }
        }
    }
}