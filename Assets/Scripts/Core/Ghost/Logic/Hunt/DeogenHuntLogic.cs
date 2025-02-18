using System.Collections.Generic;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class DeogenHuntLogic : BaseHuntLogic
    {
        private readonly IReadOnlyList<Vector3> _shelterPositions;
        
        public DeogenHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints) : base(ghostController, ghostDifficultyData,
            activityData, missionPlayersHandler, ghostFlickConfig, huntPoints)
        {
            _shelterPositions = huntPoints.Shelters;
        }

        protected override Vector3 GetHuntMovePosition()
        {
            return _shelterPositions.PickRandom();
        }
    }
}