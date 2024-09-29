namespace ElectrumGames.Core.Ghost.Logic
{
    public interface IGhostLogic
    {
        void Setup(GhostVariables variables, GhostConstants constants, int roomId);

        void FixedSimulate();
    }
}