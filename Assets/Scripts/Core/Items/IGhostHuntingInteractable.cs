namespace ElectrumGames.Core.Items
{
    public interface IGhostHuntingInteractable
    {
        bool IsOn { get; }
        bool IsGhostHuntInteractable { get; }
        bool OnGhostHuntInteractionEnter();
        void OnGhostHuntInteractionExit();
    }
}