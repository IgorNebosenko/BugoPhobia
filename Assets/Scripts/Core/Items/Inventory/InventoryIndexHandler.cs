using System;
using ElectrumGames.Configs;

namespace Core.Items.Inventory
{
    public class InventoryIndexHandler
    {
        public event Action<int> ItemIndexChanged;
        
        public int CurrentIndex { get; private set; }
        
        private readonly PlayerConfig _playerConfig;

        public InventoryIndexHandler(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public bool TrySetIndex(int index)
        {
            if (index < 0 || index >= _playerConfig.InventorySlots)
                return false;

            CurrentIndex = index;
            ItemIndexChanged?.Invoke(index);
            return true;
        }

        public void OnNextItemSelected()
        {
            if (++CurrentIndex >= _playerConfig.InventorySlots)
                CurrentIndex = 0;
            
            ItemIndexChanged?.Invoke(CurrentIndex);
        }

        public void OnPreviousItemSelected()
        {
            if (--CurrentIndex < 0)
                CurrentIndex = _playerConfig.InventorySlots - 1;
            
            ItemIndexChanged?.Invoke(CurrentIndex);
        }
    }
}