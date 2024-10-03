using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Extensions.CommonInterfaces;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public abstract class BaseGhostEvent : IGhostEventLogic
    {
        private readonly GhostController _ghostController;
        
        public bool IsInterrupt { get; private set; }

        public BaseGhostEvent(GhostController ghostController)
        {
            _ghostController = ghostController;
        }
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
        }

        public void FixedSimulate()
        {
        }

        public void GhostEventAppear(GhostAppearType appearType, bool redLight)
        {
        }

        public void GhostChasePlayer(IHavePosition player, bool isByCloud)
        {
        }
    }
}