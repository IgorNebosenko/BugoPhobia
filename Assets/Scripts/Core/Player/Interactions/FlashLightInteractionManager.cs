using System.Linq;
using Core.Items.Inventory;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.GlobalEnums;

namespace Core.Player.Interactions
{
    public class FlashLightInteractionManager : IInteractionItemsManager
    {
        private readonly IInteraction _interaction;
        private readonly FlashLightInteractionHandler _flashLightInteractionHandler;

        private readonly IInventory _inventory;
        private readonly InventoryIndexHandler _inventoryIndexHandler;

        private bool _lastState;
        private bool _flashLightState;

        public FlashLightInteractionManager(IInteraction playerInteraction, 
            FlashLightInteractionHandler flashLightInteractionHandler, IInventory inventory,
            InventoryIndexHandler inventoryIndexHandler)
        {
            _interaction = playerInteraction;
            _flashLightInteractionHandler = flashLightInteractionHandler;
            
            _inventory = inventory;
            _inventoryIndexHandler = inventoryIndexHandler;
        }

        public void Simulate(float deltaTime)
        {
            if (_interaction.FlashLightInteraction != _lastState)
            {
                _lastState = !_lastState;

                if (_lastState)
                {
                    _flashLightState = !_flashLightState;

                    if (_flashLightState)
                    {
                        if (_inventory.Items[_inventoryIndexHandler.CurrentIndex] is ItemFlashLight flashlight)
                        {
                            flashlight.OnMainInteraction();
                        }
                        else
                        {
                            var flashLights = _inventory.Items.Where(x =>
                                x != null && x.ItemType is ItemType.FlashLightSmall or ItemType.FlashLightMedium
                                    or ItemType.FlashLightBig).Select(x => x.ItemType).ToArray();

                            if (flashLights.Length != 0)
                            {
                                if (flashLights.Any(x => x == ItemType.FlashLightBig))
                                    _flashLightInteractionHandler.EnableBigLight();
                                else if (flashLights.Any(x => x == ItemType.FlashLightMedium))
                                    _flashLightInteractionHandler.EnableMediumLight();
                                else
                                    _flashLightInteractionHandler.EnableSmallLight();
                            }
                            
                        }
                    }
                    else
                    {
                        if (_inventory.Items[_inventoryIndexHandler.CurrentIndex] is ItemFlashLight flashlight)
                        {
                            flashlight.OnMainInteraction();
                        }
                        else
                        {
                            _flashLightInteractionHandler.DisableLight();
                        }
                    }
                }
            }
        }
    }
}