namespace ElectrumGames.Core.Player
{
    public interface IHavePlayerStatus
    {
        bool IsPlayablePlayer { get; }
        bool IsAlive { get; }
    }
}