using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class MareHuntLogic : BaseHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly MissionPlayersHandler _missionPlayersHandler;
        
        private bool IsLightOnRoom =>
            _ghostController.GetCurrentRoom().LightRoomHandler.IsLightOn &&
            _ghostController.GetCurrentRoom().LightRoomHandler.IsElectricityOn;
        
        
        public MareHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints, EmfData emfData, GhostEmfZonePool emfZonePool) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, ghostFlickConfig, huntPoints,
                emfData, emfZonePool)
        {
            _ghostController = ghostController;
            _missionPlayersHandler = missionPlayersHandler;
        }

        protected override bool CanHuntBySanity()
        {
            if (IsLightOnRoom)
                return _missionPlayersHandler.AverageSanity <= _ghostController.GhostEnvironmentHandler.GhostConstants
                    .modifiedSanityStartHunting;
            return _missionPlayersHandler.AverageSanity <=
                   _ghostController.GhostEnvironmentHandler.GhostConstants.defaultSanityStartHunting;
        }
    }
}