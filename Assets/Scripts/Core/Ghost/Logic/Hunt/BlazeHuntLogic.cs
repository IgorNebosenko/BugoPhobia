using ElectrumGames.Core.Ghost.Controllers;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class BlazeHuntLogic : IHuntLogic
    {
        private readonly GhostController _ghostController;

        public BlazeHuntLogic(GhostController ghostController)
        {
            _ghostController = ghostController;
        }

        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
        }

        public void FixedSimulate()
        {
        }

        public bool TryHunt()
        {
            return false;
        }
    }
}