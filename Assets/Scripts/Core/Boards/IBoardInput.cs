namespace ElectrumGames.Core.Boards
{
    public interface IBoardInput
    {
        bool Return { get; }
        bool Open { get; }
        bool MoveRight { get; }
        bool MoveLeft { get; }
    }
}