using System.Collections.Generic;
using Core.Items.Inventory;
using ElectrumGames.Core.Player.Interactions;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Core.PlayerVisuals;

namespace ElectrumGames.Core.Player
{
    public class Player : PlayerBase
    {
        private List<IInteractionItemsManager> _interactionItemsManagers;
        
        private PutInteractionHandler _putInteractionHandler;
        private DropInteractionHandler _dropInteractionHandler;
        private SelectInventorySlotHandler _selectInventorySlotHandler;
        private ItemInteractionVisual _itemInteractionVisual;
        private ItemInteractionHandler _interactionHandler;
        private ExternalInteractionManager _externalInteractionManager;
        private FlashLightInteractionManager _flashLightInteractionManager;
        
        protected override void OnAfterSpawn()
        {
            InventoryIndexHandler = new InventoryIndexHandler(playerConfig);
            
            interaction = new PlayerInteraction(inputActions, InventoryIndexHandler);
            interaction.Init();
            
            _itemInteractionVisual = new ItemInteractionVisual(Inventory, InventoryIndexHandler, itemsConfig, 
                transform, playerCamera.transform);
            
            _putInteractionHandler = new PutInteractionHandler(interaction, playerCamera, playerConfig, 
                Inventory, InventoryIndexHandler, _itemInteractionVisual);

            _dropInteractionHandler =
                new DropInteractionHandler(interaction, playerCamera.transform, Inventory, InventoryIndexHandler);

            _selectInventorySlotHandler = new SelectInventorySlotHandler(interaction, InventoryIndexHandler);

            _interactionHandler = new ItemInteractionHandler(interaction, Inventory, InventoryIndexHandler);

            _externalInteractionManager = new ExternalInteractionManager(interaction, input, playerCamera, playerConfig);

            _flashLightInteractionManager = new FlashLightInteractionManager(interaction, flashLightInteractionHandler,
                Inventory, InventoryIndexHandler, _itemInteractionVisual);

            _interactionItemsManagers = new List<IInteractionItemsManager>
            {
                _putInteractionHandler,
                _dropInteractionHandler,
                _selectInventorySlotHandler,
                _itemInteractionVisual,
                _interactionHandler,
                _externalInteractionManager,
                _flashLightInteractionManager
            };
            
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
            for (var i = 0; i < _interactionItemsManagers.Count; i++)
                _interactionItemsManagers[i].Simulate(deltaTime);
        }
    }
}