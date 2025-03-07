using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class EtherealHuntLogic : BaseHuntLogic
    {
        public EtherealHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints, EmfData emfData, GhostEmfZonePool emfZonePool) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, ghostFlickConfig, huntPoints,
                emfData, emfZonePool)
        {
            Debug.LogError("Ethereal must have non-standard hunt logic!");
        }
    }
}