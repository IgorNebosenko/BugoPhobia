using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemEmfElectronic : ItemInstanceBase, 
        IGhostHuntingHasElectricity, IGhostHuntingInteractableStay, IGhostHuntingInteractableExit
    {
        private bool _isOn;
        public bool IsElectricityOn => _isOn;
        
        public override void OnMainInteraction()
        {
            _isOn = !_isOn;
        }

        public override void OnAlternativeInteraction()
        {
        }
        
        public void OnGhostInteractionStay()
        {
        }

        public void OnGhostInteractionExit()
        {
        }
    }
}