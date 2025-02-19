using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class PoltergeistHuntLogic : BaseHuntLogic
    {
        public override float ChanceThrowItem => 1f;
        public override float ChanceTouchDoor => 1f;

        public PoltergeistHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints) : base(ghostController, ghostDifficultyData,
            activityData, missionPlayersHandler, ghostFlickConfig, huntPoints)
        {
        }
    }
}