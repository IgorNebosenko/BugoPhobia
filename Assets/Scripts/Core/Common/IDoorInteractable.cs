namespace ElectrumGames.Core.Common
{
    public interface IDoorInteractable
    {
        bool DoorWithLock { get; }
        void TouchDoor(float angle, float time);
    }
}