using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class BlazeBaseGhostEventLogic : BaseGhostEvent
    {
        public BlazeBaseGhostEventLogic(GhostController ghostController, GhostDifficultyData difficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData) :
            base(ghostController, difficultyData, activityData, emfZonesPool,emfData)
        {
        }
    }
}