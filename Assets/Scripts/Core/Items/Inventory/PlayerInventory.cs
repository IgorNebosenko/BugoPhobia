using System.Collections.Generic;
using ElectrumGames.Core.Items;

namespace ElectrumGames.Core.Items.Inventory
{
    public class PlayerInventory : IInventory
    {
        private List<ItemInstanceBase> _listItems;
        private int _countItems;
        
        public IReadOnlyList<ItemInstanceBase> Items => _listItems;
        
        public void Init(int countItems)
        {
            _countItems = countItems;
            _listItems = new List<ItemInstanceBase>();

            for (var i = 0; i < _countItems; i++)
            {
                _listItems.Add(null);
            }
        }

        public bool TryAddItem(ItemInstanceBase item, int slot)
        {
            if (slot < 0 || slot >= _listItems.Count || _listItems[slot] != null)
                return false;

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