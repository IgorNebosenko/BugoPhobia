using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Core.Items.Inventory
{
    public class PlayerInventory : IInventory
    {
        private List<ItemInstanceBase> _listItems;
        private int _countItems;

        private Transform _parent;
        
        public IReadOnlyList<ItemInstanceBase> Items => _listItems;
        
        public void Init(int countItems, Transform parent)
        {
            _countItems = countItems;
            _parent = parent;
            
            _listItems = new List<ItemInstanceBase>();

            for (var i = 0; i < _countItems; i++)
            {
                _listItems.Add(null);
            }
        }

        public bool TryAddItem(ItemInstanceBase item, int slot)
        {
            if (slot < 0 || slot >= _listItems.Count)
                return false;

            if (_listItems[slot] != null)
            {
                slot = _listItems.IndexOf(null);
                if (slot == -1)
                    return false;
            }


            item.transform.parent = _parent;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(false);
            
            _listItems[slot] = item;
            
            return true;
        }

        public ItemInstanceBase RealizeItem(int slot)
        {
            if (slot < 0 || slot >= _listItems.Count)
                return null;
            
            var item = _listItems[slot];
            _listItems[slot] = null;
            return item;
        }
    }
}