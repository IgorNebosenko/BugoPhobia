﻿using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class WraithHuntLogic : BaseHuntLogic
    {
        public override bool HasFootSteps => false;
        
        public WraithHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints) : base(ghostController, ghostDifficultyData,
            activityData, missionPlayersHandler, ghostFlickConfig, huntPoints)
        {
        }
    }
}