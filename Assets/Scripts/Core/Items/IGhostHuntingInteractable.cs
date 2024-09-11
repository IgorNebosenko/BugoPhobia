namespace ElectrumGames.Core.Items
{
    public interface IGhostHuntingInteractable
    {
        bool IsElectricityOn { get; }
        void OnGhostHuntInteractionEnter();
        void OnGhostHuntInteractionExit();
    }
}