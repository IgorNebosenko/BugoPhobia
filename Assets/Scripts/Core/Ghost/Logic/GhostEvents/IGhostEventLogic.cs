using ElectrumGames.Extensions.CommonInterfaces;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public interface IGhostEventLogic : IGhostLogic
    {
        bool CheckIsPlayerNear();
    }
}