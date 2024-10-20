using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class LostSoulHuntLogic : BaseHuntLogic
    {
        public LostSoulHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints) : base(ghostController, ghostDifficultyData,
            activityData, missionPlayersHandler, ghostFlickConfig, huntPoints)
        {
            Debug.LogError("Lost soul must have non-standard hunt logic!");
        }
    }
}