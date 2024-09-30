using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class BlazeNonHuntLogic : BaseNonHuntLogic
    {
        public BlazeNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData) : base(ghostController, ghostDifficultyData, activityData)
        {
        }
    }
}