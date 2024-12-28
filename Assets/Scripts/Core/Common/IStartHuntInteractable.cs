namespace ElectrumGames.Core.Common
{
    public interface IStartHuntInteractable
    {
        int CountUsesRemain { get; }
        bool OnHuntInteraction();
    }
}