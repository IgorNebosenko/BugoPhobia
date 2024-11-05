using ElectrumGames.Core.Items.Inventory;

namespace ElectrumGames.Core.Player
{
    public interface IHaveInventory
    {
        IInventory Inventory { get; }
        InventoryIndexHandler InventoryIndexHandler { get; }
    }
}