﻿using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class PoltergeistGhostEventLogic : BaseGhostEvent
    {
        public PoltergeistGhostEventLogic(GhostController ghostController, GhostDifficultyData difficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData,
            MissionPlayersHandler missionPlayersHandler) : base(ghostController, difficultyData,
            activityData, emfZonesPool, emfData, missionPlayersHandler)
        {
        }
    }
}