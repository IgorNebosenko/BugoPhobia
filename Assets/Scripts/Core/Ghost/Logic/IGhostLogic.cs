namespace ElectrumGames.Core.Ghost.Logic
{
    public interface IGhostLogic
    {
        bool IsInterrupt { get; set; }
        void Setup(GhostVariables variables, GhostConstants constants, int roomId);

        void FixedSimulate();
    }
}