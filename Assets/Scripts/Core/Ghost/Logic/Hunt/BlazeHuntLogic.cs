using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Missions;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class BlazeHuntLogic : BaseHuntLogic
    {
        public BlazeHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler)
        {
        }
    }
}