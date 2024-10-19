using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class BabadukNonHuntLogic : BaseNonHuntLogic
    {
        public BabadukNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData) : base(ghostController,
            ghostDifficultyData, activityData, emfZonesPool, emfData)
        {
            Debug.LogError("Babaduk must be stronger if deselected in journal!");
        }
    }
}