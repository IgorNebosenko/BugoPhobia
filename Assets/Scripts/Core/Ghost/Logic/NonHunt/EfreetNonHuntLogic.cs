using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class EfreetNonHuntLogic : BaseNonHuntLogic
    {
        public EfreetNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, GhostEmfZonePool ghostEmfZonePool, EmfData emfData) : 
            base(ghostController, ghostDifficultyData, activityData, ghostEmfZonePool, emfData)
        {
        }
    }
}