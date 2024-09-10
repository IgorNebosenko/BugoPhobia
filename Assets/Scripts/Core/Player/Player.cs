using Core.Items.Inventory;
using Core.Player.Interactions;
using ElectrumGames.Core.PlayerVisuals;

namespace ElectrumGames.Core.Player
{
    public class Player : PlayerBase
    {
        private PutInteractionHandler _putInteractionHandler;
        private InventoryIndexHandler _inventoryIndexHandler;
        
        protected override void OnAfterSpawn()
        {
            _inventoryIndexHandler = new InventoryIndexHandler(playerConfig);
            
            _putInteractionHandler = new PutInteractionHandler(interaction, playerCamera, playerConfig, 
                inventory, _inventoryIndexHandler);
            
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