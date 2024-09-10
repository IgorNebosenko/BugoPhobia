using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Core.Items.Inventory
{
    public interface IInventory
    {
        IReadOnlyList<ItemInstanceBase> Items { get; }

        void Init(int countItems, Transform parent, int playerNetId);

        bool TryAddItem(ItemInstanceBase item, int slot);
        ItemInstanceBase RealizeItem(int slot);
    }
}