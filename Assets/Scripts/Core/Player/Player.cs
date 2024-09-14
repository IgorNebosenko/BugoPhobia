using System.Collections.Generic;
using Core.Items.Inventory;
using Core.Player.Interactions;
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
        
        private InventoryIndexHandler _inventoryIndexHandler;
        
        protected override void OnAfterSpawn()
        {
            _inventoryIndexHandler = new InventoryIndexHandler(playerConfig);
            
            interaction = new PlayerInteraction(inputActions, _inventoryIndexHandler);
            interaction.Init();
            
            _itemInteractionVisual = new ItemInteractionVisual(Inventory, _inventoryIndexHandler, itemsConfig, 
                transform, playerCamera.transform);
            
            _putInteractionHandler = new PutInteractionHandler(interaction, playerCamera, playerConfig, 
                Inventory, _inventoryIndexHandler, _itemInteractionVisual);

            _dropInteractionHandler =
                new DropInteractionHandler(interaction, playerCamera.transform, Inventory, _inventoryIndexHandler);

            _selectInventorySlotHandler = new SelectInventorySlotHandler(interaction, _inventoryIndexHandler);

            _interactionHandler = new ItemInteractionHandler(interaction, Inventory, _inventoryIndexHandler);

            _externalInteractionManager = new ExternalInteractionManager(interaction, input, playerCamera, playerConfig);

            _interactionItemsManagers = new List<IInteractionItemsManager>
            {
                _putInteractionHandler,
                _dropInteractionHandler,
                _selectInventorySlotHandler,
                _itemInteractionVisual,
                _interactionHandler,
                _externalInteractionManager
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