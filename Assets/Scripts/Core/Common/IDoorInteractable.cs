namespace ElectrumGames.Core.Common
{
    public interface IDoorInteractable : IGhostInteractable
    {
        bool DoorWithLock { get; }
        void TouchDoor(float angle, float time, bool hasEvidence);
    }
}