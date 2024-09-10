using ElectrumGames.CommonInterfaces;

namespace ElectrumGames.Core.Items.Inventory
{
    public interface IItem : IHaveNetId
    {
        void OnMainInteract();
        void OnAlternativeInteract();
    }
}
