using ElectrumGames.CommonInterfaces;

namespace ElectrumGames.Core.Inventory
{
    public interface IItem : IHaveNetId
    {
        void OnMainInteract();
        void OnAlternativeInteract();
    }
}
