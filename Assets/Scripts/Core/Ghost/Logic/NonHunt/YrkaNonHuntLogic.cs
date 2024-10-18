using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class YrkaNonHuntLogic : BaseNonHuntLogic
    {
        public YrkaNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData) : 
            base(ghostController, ghostDifficultyData, activityData, emfZonesPool, emfData)
        {
        }
    }
}