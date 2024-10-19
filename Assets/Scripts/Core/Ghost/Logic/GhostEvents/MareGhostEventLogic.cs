using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class MareGhostEventLogic : BaseGhostEvent
    {
        public MareGhostEventLogic(GhostController ghostController, GhostDifficultyData difficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData,
            MissionPlayersHandler missionPlayersHandler) : base(ghostController, difficultyData, activityData,
            emfZonesPool, emfData, missionPlayersHandler)
        {
            Debug.LogError("Mare must have different ghost event activity on light");
        }
    }
}