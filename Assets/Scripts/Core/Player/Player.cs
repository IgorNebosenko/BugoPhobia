using Core.Player.Interactions;
using ElectrumGames.Core.PlayerVisuals;

namespace ElectrumGames.Core.Player
{
    public class Player : PlayerBase
    {
        private ExternalInteractionHandler _externalInteractionHandler;
        protected override void OnAfterSpawn()
        {
            _externalInteractionHandler = new ExternalInteractionHandler(interaction, playerCamera, playerConfig);
            
            var headBob = new HeadBobVisual();
            headBob.SetCamera(playerCamera);
            
            simulateVisuals.Add(headBob);

            foreach (var simulateVisual in simulateVisuals)
            {
                simulateVisual.Init(configService, playerConfig);
            }
        }

        protected override void OnInteractionSimulate(float deltaTime)
        {
            _externalInteractionHandler.Simulate(deltaTime);
        }
    }
}