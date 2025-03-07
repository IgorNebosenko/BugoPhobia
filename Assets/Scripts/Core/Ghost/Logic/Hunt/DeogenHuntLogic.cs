using System.Collections.Generic;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
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
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints, EmfData emfData, GhostEmfZonePool emfZonePool) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, ghostFlickConfig, huntPoints,
                emfData, emfZonePool)
        {
            _shelterPositions = huntPoints.Shelters;
        }

        protected override Vector3 GetHuntMovePosition()
        {
            return _shelterPositions.PickRandom();
        }
    }
}