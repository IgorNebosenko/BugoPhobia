using ElectrumGames.Core.Items.Inventory;

namespace ElectrumGames.Core.Player.Interactions
{
    public class SelectInventorySlotHandler : IInteractionItemsManager
    {
        private readonly IInteraction _interactions;
        private readonly InventoryIndexHandler _inventoryIndexHandler;

        private bool _lastFirstState;
        private bool _lastSecondState;
        private bool _lastThirdState;
        private bool _lastFourthState;
        private bool _lastNextIndexState;

        public SelectInventorySlotHandler(IInteraction interactions, InventoryIndexHandler inventoryIndexHandler)
        {
            _interactions = interactions;
            _inventoryIndexHandler = inventoryIndexHandler;
        }

        public void Simulate(float deltaTime)
        {
            if (_lastFirstState != _interactions.FirstSlotSelected)
            {
                _lastFirstState = !_lastFirstState;
                if (_lastFirstState)
                    _inventoryIndexHandler.TrySetIndex(0);
            }

            if (_lastSecondState != _interactions.SecondSlotSelected)
            {
                _lastSecondState = !_lastSecondState;
                if (_lastSecondState)
                    _inventoryIndexHandler.TrySetIndex(1);
            }
            
            if (_lastThirdState != _interactions.ThirdSlotSelected)
            {
                _lastThirdState = !_lastThirdState;
                if (_lastThirdState)
                    _inventoryIndexHandler.TrySetIndex(2);
            }
            
            if (_lastFourthState != _interactions.FourthSlotSelected)
            {
                _lastFourthState = !_lastFourthState;
                if (_lastFourthState)
                    _inventoryIndexHandler.TrySetIndex(3);
            }

            if (_lastNextIndexState != _interactions.NextSlotSelected)
            {
                _lastNextIndexState = !_lastNextIndexState;
                if (_lastNextIndexState)
                    _inventoryIndexHandler.OnNextItemSelected();
            }
        }
    }
}