using ElectrumGames.Extensions.CommonInterfaces;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public interface IGhostEventLogic : IGhostLogic
    {
        void GhostEventAppear(GhostAppearType appearType, bool redLight);
        void GhostChasePlayer(IHavePosition player, bool isByCloud);
    }
}