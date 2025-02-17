using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class YureiGhostEventLogic : BaseGhostEvent
    {
        private readonly GhostController _ghostController;
        private readonly MissionPlayersHandler _missionPlayersHandler;
        
        protected override float GhostEventsFrequency =>
            Mathf.Lerp(_ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents / 2,
                _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents,
                (100f - _missionPlayersHandler.AverageSanity) * 0.01f);

        public YureiGhostEventLogic(GhostController ghostController, GhostDifficultyData difficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData,
            MissionPlayersHandler missionPlayersHandler) : base(ghostController, difficultyData, activityData,
            emfZonesPool, emfData, missionPlayersHandler)
        {
            _ghostController = ghostController;
            _missionPlayersHandler = missionPlayersHandler;
        }
    }
}