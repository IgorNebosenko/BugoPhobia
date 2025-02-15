using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class MareGhostEventLogic : BaseGhostEvent
    {
        private const float LightModifier = 2f;
        
        private readonly GhostController _ghostController;

        private bool IsLightOnRoom =>
            _ghostController.GetCurrentRoom().LightRoomHandler.IsLightOn &&
            _ghostController.GetCurrentRoom().LightRoomHandler.IsElectricityOn;
        
        protected override float GhostEventsFrequency
        {
            get
            {
                if (IsLightOnRoom)
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents / LightModifier;
                return _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents;
            }
        }
        
        public MareGhostEventLogic(GhostController ghostController, GhostDifficultyData difficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData,
            MissionPlayersHandler missionPlayersHandler) : base(ghostController, difficultyData, activityData,
            emfZonesPool, emfData, missionPlayersHandler)
        {
            _ghostController = ghostController;
        }
    }
}