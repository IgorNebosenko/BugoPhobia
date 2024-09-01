using System.Collections.Generic;

namespace ElectrumGames.Core.Inventory
{
    public interface IInventory
    {
        List<IItem> Items { get; }

        void Init(int countItems);
    }
}