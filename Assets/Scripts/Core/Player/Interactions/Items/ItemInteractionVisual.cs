using System;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using UnityEngine;

namespace ElectrumGames.Core.Player.Interactions.Items
{
    public class ItemInteractionVisual : IInteractionItemsManager
    {
        private readonly IInventory _inventory;
        private readonly InventoryIndexHandler _inventoryIndexHandler;
        private readonly ItemsConfig _itemsConfig;

        private readonly Transform _parentPlayer;
        private readonly Transform _parentHand;

        public event Action<ItemInstanceBase> ItemChanged;

        private int _lastIndex = -1;

        public ItemInteractionVisual(IInventory inventory, InventoryIndexHandler inventoryIndexHandler, ItemsConfig itemsConfig,
            Transform parentPlayer, Transform parentHand)
        {
            _inventory = inventory;
            _inventoryIndexHandler = inventoryIndexHandler;
            _itemsConfig = itemsConfig;

            _parentPlayer = parentPlayer;
            _parentHand = parentHand;
        }

        public void Simulate(float deltaTime)
        {
            if (_lastIndex == _inventoryIndexHandler.CurrentIndex)
                return;
            
            ItemChanged?.Invoke(_inventory.Items[_inventoryIndexHandler.CurrentIndex]);

            if (_lastIndex >= 0 && _lastIndex < _inventory.Items.Count && _inventory.Items[_lastIndex] != null)
            {
                _inventory.Items[_lastIndex].transform.parent = _parentPlayer;
                _inventory.Items[_lastIndex].transform.position = Vector3.zero;
                _inventory.Items[_lastIndex].transform.rotation = Quaternion.identity;
                _inventory.Items[_lastIndex].transform.localScale = _inventory.Items[_lastIndex].LocalScale;
                _inventory.Items[_lastIndex].gameObject.SetActive(false);
                _inventory.Items[_lastIndex].PhysicObject.isKinematic = false;
                _inventory.Items[_lastIndex].Collider.enabled = true;
            }

            _lastIndex = _inventoryIndexHandler.CurrentIndex;

            if (_inventory.Items[_lastIndex] != null)
            {
                var configData = _itemsConfig.GetItemByType(_inventory.Items[_lastIndex].ItemType);

                _inventory.Items[_lastIndex].transform.parent = _parentHand;
                _inventory.Items[_lastIndex].transform.localPosition = configData.UserPositionAtCamera;
                _inventory.Items[_lastIndex].transform.localRotation = configData.UserRotationAtCamera;
                _inventory.Items[_lastIndex].transform.localScale = _inventory.Items[_lastIndex].LocalScale;
                _inventory.Items[_lastIndex].gameObject.SetActive(true);
                _inventory.Items[_lastIndex].PhysicObject.isKinematic = true;
                _inventory.Items[_lastIndex].Collider.enabled = false;
            }
        }

        public void ForceUpdate()
        {
            _lastIndex = -1;
        }
    }
}