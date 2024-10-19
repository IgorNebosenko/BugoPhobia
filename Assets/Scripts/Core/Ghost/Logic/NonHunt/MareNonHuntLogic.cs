using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class MareNonHuntLogic : BaseNonHuntLogic
    {
        public MareNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData) : base(ghostController,
            ghostDifficultyData, activityData, emfZonesPool, emfData)
        {
            Debug.LogError("Mare must have different non-hunt logic at light/no light");
        }
    }
}