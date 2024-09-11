using Core.Items.Inventory;
using Core.Player.Interactions;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Core.PlayerVisuals;

namespace ElectrumGames.Core.Player
{
    public class Player : PlayerBase
    {
        private PutInteractionHandler _putInteractionHandler;
        private DropInteractionHandler _dropInteractionHandler;
        private SelectInventorySlotHandler _selectInventorySlotHandler;
        private ItemInteractionVisual _itemInteractionVisual;
        
        private InventoryIndexHandler _inventoryIndexHandler;
        
        protected override void OnAfterSpawn()
        {
            _inventoryIndexHandler = new InventoryIndexHandler(playerConfig);
            
            interaction = new PlayerInteraction(inputActions, _inventoryIndexHandler);
            interaction.Init();
            
            _itemInteractionVisual = new ItemInteractionVisual(inventory, _inventoryIndexHandler, itemsConfig, 
                transform, playerCamera.transform);
            
            _putInteractionHandler = new PutInteractionHandler(interaction, playerCamera, playerConfig, 
                inventory, _inventoryIndexHandler, _itemInteractionVisual);

            _dropInteractionHandler =
                new DropInteractionHandler(interaction, playerCamera.transform, inventory, _inventoryIndexHandler);

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
            _dropInteractionHandler.Simulate(deltaTime);
            _itemInteractionVisual.Simulate(deltaTime);
        }
    }
}