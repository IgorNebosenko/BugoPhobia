using System.Collections.Generic;

namespace ElectrumGames.Core.Items.Inventory
{
    public interface IInventory
    {
        IReadOnlyList<ItemInstanceBase> Items { get; }

        void Init(int countItems);

        bool TryAddItem(ItemInstanceBase item, int slot);
        ItemInstanceBase RealizeItem(int slot);
    }
}