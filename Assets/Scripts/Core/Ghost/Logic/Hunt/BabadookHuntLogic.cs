using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class BabadookHuntLogic : BaseHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly MissionPlayersHandler _missionPlayersHandler;
        
        public BabadookHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
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
            if(_ghostController.JournalManager.PlayerJournalInstance.SelectedGhost == GhostType.Babadook ||
                   _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.SelectedGhost == GhostType.Babadook))
                return _missionPlayersHandler.AverageSanity < _ghostController.GhostEnvironmentHandler.GhostConstants.modifiedSanityStartHunting;
            
            return _missionPlayersHandler.AverageSanity < _ghostController.GhostEnvironmentHandler.GhostConstants.defaultSanityStartHunting;
        }
    }
}