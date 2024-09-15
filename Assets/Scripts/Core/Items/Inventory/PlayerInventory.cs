using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Core.Items.Inventory
{
    public class PlayerInventory : IInventory
    {
        private int _countItems;

        private Transform _parent;

        private int _playerNetId;
        
        public List<ItemInstanceBase> Items { get; private set; }
        
        public void Init(int countItems, Transform parent, int playerNetId)
        {
            _countItems = countItems;
            _parent = parent;
            _playerNetId = playerNetId;
            
            Items = new List<ItemInstanceBase>();

            for (var i = 0; i < _countItems; i++)
            {
                Items.Add(null);
            }
        }

        public bool TryAddItem(ItemInstanceBase item, int slot)
        {
            if (slot < 0 || slot >= Items.Count)
                return false;

            if (Items[slot] != null)
            {
                slot = Items.IndexOf(null);
                if (slot == -1)
                    return false;
            }
            
            item.transform.parent = _parent;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = Vector3.one;
            item.gameObject.SetActive(false);
            item.SetNetId(item.NetId, _playerNetId);
            item.SetInventory(this);
            
            Items[slot] = item;
            
            return true;
        }

        public ItemInstanceBase RealizeItem(int slot)
        {
            if (slot < 0 || slot >= Items.Count)
                return null;
            
            var item = Items[slot];
            Items[slot] = null;
            return item;
        }
    }
}