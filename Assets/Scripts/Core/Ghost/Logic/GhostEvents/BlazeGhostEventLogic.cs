using ElectrumGames.Core.Ghost.Controllers;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class BlazeGhostEventLogic : IGhostEventLogic
    {
        private readonly GhostController _ghostController;

        public BlazeGhostEventLogic(GhostController ghostController)
        {
            _ghostController = ghostController;
        }

        public void Setup(GhostVariables variables, GhostConstants constants)
        {
        }

        public void FixedSimulate()
        {
        }

        public bool TryGhostEvent()
        {
            return false;
        }
    }
}