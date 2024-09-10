using Core.Player.Interactions;
using ElectrumGames.Core.PlayerVisuals;

namespace ElectrumGames.Core.Player
{
    public class Player : PlayerBase
    {
        private PutInteractionHandler _putInteractionHandler;
        protected override void OnAfterSpawn()
        {
            _putInteractionHandler = new PutInteractionHandler(interaction, playerCamera, playerConfig, inventory);
            
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
            _putInteractionHandler.Simulate(deltaTime);
        }
    }
}