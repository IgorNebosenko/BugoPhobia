namespace ElectrumGames.Core.Common
{
    public interface IGhostHuntingInteractable
    {
        bool IsElectricityOn { get; }
        void OnGhostInteractionStay();
    }
}