using System.Collections.Generic;
using System.Linq;
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
    public class LostSoulHuntLogic : BaseHuntLogic
    {
        private IReadOnlyList<Vector3> _positions;
        private int _index;
        
        public LostSoulHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints, EmfData emfData, GhostEmfZonePool emfZonePool) :
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, ghostFlickConfig, huntPoints,
                emfData, emfZonePool)
        {
            var list = huntPoints.Positions.Select(x => x.position).ToList();
            list.AddRange(huntPoints.Shelters);
            list.Shuffle();
            
            _positions = list;
        }

        protected override Vector3 GetHuntMovePosition()
        {
            _index++;

            if (_positions.Count <= _index)
                _index = 0;

            return _positions[_index];
        }

        protected override void StopHunt()
        {
            _index = 0;
            base.StopHunt();
        }
    }
}