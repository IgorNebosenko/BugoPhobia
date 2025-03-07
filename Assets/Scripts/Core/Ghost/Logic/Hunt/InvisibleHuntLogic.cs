using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class InvisibleHuntLogic : BaseHuntLogic
    {
        public InvisibleHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints, EmfData emfData, GhostEmfZonePool emfZonePool) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, ghostFlickConfig, huntPoints,
                emfData, emfZonePool)
        {
            Debug.LogError("Invisible must make footprints during hunt!");
        }
    }
}