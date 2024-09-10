using Core.Items.Inventory;
using Core.Player.Interactions;
using ElectrumGames.Core.PlayerVisuals;

namespace ElectrumGames.Core.Player
{
    public class Player : PlayerBase
    {
        private PutInteractionHandler _putInteractionHandler;
        private SelectInventorySlotHandler _selectInventorySlotHandler;
        
        private InventoryIndexHandler _inventoryIndexHandler;
        
        protected override void OnAfterSpawn()
        {
            _inventoryIndexHandler = new InventoryIndexHandler(playerConfig);
            
            interaction = new PlayerInteraction(_inputActions, _inventoryIndexHandler);
            interaction.Init();
            
            _putInteractionHandler = new PutInteractionHandler(interaction, playerCamera, playerConfig, 
                inventory, _inventoryIndexHandler);

            _selectInventorySlotHandler = new SelectInventorySlotHandler(interaction, _inventoryIndexHandler);
            
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
            _selectInventorySlotHandler.Simulate(deltaTime);
        }
    }
}