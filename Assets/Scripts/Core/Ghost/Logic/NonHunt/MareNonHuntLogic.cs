using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class MareNonHuntLogic : BaseNonHuntLogic
    {
        private const float LightModifier = 2f;
        
        private readonly GhostController _ghostController;

        private bool IsLightOnRoom =>
            _ghostController.GetCurrentRoom().LightRoomHandler.IsLightOn &&
            _ghostController.GetCurrentRoom().LightRoomHandler.IsElectricityOn;

        protected override float DoorsInteractions
        {
            get
            {
                if (IsLightOnRoom)
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.doorsInteractions / LightModifier;
                return _ghostController.GhostEnvironmentHandler.GhostVariables.doorsInteractions;
            }
        }

        protected override float SwitchesInteractions
        {
            get
            {
                if (IsLightOnRoom)
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.switchesInteractions / LightModifier;
                return _ghostController.GhostEnvironmentHandler.GhostVariables.switchesInteractions;
            }
        }

        protected override float ThrowsInteractions
        {
            get
            {
                if (IsLightOnRoom)
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.throws / LightModifier;
                return _ghostController.GhostEnvironmentHandler.GhostVariables.throws;
            }
        }

        protected override float OtherInteractions
        {
            get
            {
                if (IsLightOnRoom)
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.otherInteractions / LightModifier;
                return _ghostController.GhostEnvironmentHandler.GhostVariables.otherInteractions;
            }
        }

        public MareNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData) : base(ghostController,
            ghostDifficultyData, activityData, emfZonesPool, emfData)
        {
            _ghostController = ghostController;
        }
    }
}