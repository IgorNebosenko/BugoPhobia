using System.Collections.Generic;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class InteractionAura : MonoBehaviour
    {
        private List<IDoorInteractable> _doorsInTrigger = new ();
        private List<ISwitchInteractable> _switchesInTrigger = new ();
        private List<IGhostThrowable> _thrownInTrigger = new ();
        private List<IGhostOtherInteraction> _otherInteractionsInTrigger = new ();

        public IReadOnlyList<IDoorInteractable> DoorsInTrigger => _doorsInTrigger;
        public IReadOnlyList<ISwitchInteractable> SwitchesInTrigger => _switchesInTrigger;
        public IReadOnlyList<IGhostThrowable> ThrownInTrigger => _thrownInTrigger;
        public IReadOnlyList<IGhostOtherInteraction> OtherInteractionsInTrigger => _otherInteractionsInTrigger;

        public IFuseBoxInteractable FuseBox { get; private set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDoorInteractable>(out var door))
                _doorsInTrigger.Add(door);
            
            if (other.TryGetComponent<ISwitchInteractable>(out var switchItem))
                _switchesInTrigger.Add(switchItem);
            
            if (other.TryGetComponent<IGhostThrowable>(out var thrownObject))
                _thrownInTrigger.Add(thrownObject);
            
            if (other.TryGetComponent<IGhostOtherInteraction>(out var otherInteraction))
                _otherInteractionsInTrigger.Add(otherInteraction);

            if (other.TryGetComponent<IFuseBoxInteractable>(out var fuseBox))
                FuseBox = fuseBox;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDoorInteractable>(out var door))
                _doorsInTrigger.Remove(door);
            
            if (other.TryGetComponent<ISwitchInteractable>(out var switchItem))
                _switchesInTrigger.Remove(switchItem);
            
            if (other.TryGetComponent<IGhostThrowable>(out var thrownObject))
                _thrownInTrigger.Remove(thrownObject);
            
            if (other.TryGetComponent<IGhostOtherInteraction>(out var otherInteraction))
                _otherInteractionsInTrigger.Remove(otherInteraction);

            if (other.TryGetComponent<IFuseBoxInteractable>(out _))
                FuseBox = null;
        }
    }
}