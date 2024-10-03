using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class SwitchableLampEnvironmentObject : LightEnvironmentObject, IGhostOtherInteraction
    {
        private bool _lastState;

        public Transform Transform => transform;
        
        public override void OnInteract()
        {
            _lastState = !_lastState;
            
            SwitchStateTo(_lastState);
        }
        
        public void Interact()
        {
            OnInteract();
        }
    }
}