using System.Linq;
using Core.Items.Inventory;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Extensions;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Player.Interactions.Items
{
    public class FlashLightInteractionManager : IInteractionItemsManager
    {
        private readonly IInteraction _interaction;
        private readonly FlashLightInteractionHandler _flashLightInteractionHandler;

        private readonly IInventory _inventory;
        private readonly InventoryIndexHandler _inventoryIndexHandler;

        private readonly ItemInteractionVisual _itemInteractionVisual;

        private bool _lastState;
        private bool _flashLightState;

        public FlashLightInteractionManager(IInteraction playerInteraction, 
            FlashLightInteractionHandler flashLightInteractionHandler, IInventory inventory,
            InventoryIndexHandler inventoryIndexHandler, ItemInteractionVisual itemInteractionVisual)
        {
            _interaction = playerInteraction;
            _flashLightInteractionHandler = flashLightInteractionHandler;
            
            _inventory = inventory;
            _inventoryIndexHandler = inventoryIndexHandler;

            _itemInteractionVisual = itemInteractionVisual;

            _itemInteractionVisual.ItemChanged += OnItemChanged;
        }

        ~FlashLightInteractionManager()
        {
            _itemInteractionVisual.ItemChanged -= OnItemChanged;
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
                            SelectMostPowerfulFlashlight(true);
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

<<<<<<< HEAD:Assets/Scripts/Core/Player/Interactions/FlashLightInteractionManager.cs
        public void OnGhostInterferenceStay()
        {
            _flashLightInteractionHandler.OnGhostInterferenceStay();
=======
        private void SelectMostPowerfulFlashlight(bool isSkipEnabled)
        {
            var flashLights = _inventory.Items.Where(x =>
                x != null && x.ItemType is ItemType.FlashLightSmall or ItemType.FlashLightMedium
                    or ItemType.FlashLightBig && (isSkipEnabled || ((ItemFlashLight)x).IsElectricityOn)).
                Select(x => x.ItemType).ToArray();

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
        
        private void OnItemChanged(ItemInstanceBase item)
        {
            if (item.UnityNullCheck())
            {
                SelectMostPowerfulFlashlight(false);
                return;
            }

            if (item.ItemType is ItemType.FlashLightSmall or ItemType.FlashLightMedium or ItemType.FlashLightBig)
                _flashLightInteractionHandler.DisableLight();
            else
                SelectMostPowerfulFlashlight(false);

>>>>>>> b3dc64eca3cc6d5155e7acc4108061b0f5dafed5:Assets/Scripts/Core/Player/Interactions/Items/FlashLightInteractionManager.cs
        }
    }
}