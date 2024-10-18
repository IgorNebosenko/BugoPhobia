using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class BlazeHuntLogic : BaseHuntLogic
    {
        public BlazeHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler, GhostFlickConfig flickConfig,
            HuntPoints huntPoints) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, flickConfig, huntPoints)
        {
            Debug.LogError("Blaze must to have non-standard hunt logic!");
        }
    }
}