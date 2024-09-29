namespace ElectrumGames.Core.Ghost.Logic
{
    public interface IGhostLogic
    {
        bool IsInterrupt { get; }
        void Setup(GhostVariables variables, GhostConstants constants, int roomId);

        void FixedSimulate();
    }
}