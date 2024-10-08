using ElectrumGames.Core.Player;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public interface IGhostEventLogic : IGhostLogic
    {
        IPlayer GetNearPlayer();
    }
}