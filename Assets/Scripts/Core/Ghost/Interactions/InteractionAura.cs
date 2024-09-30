using System.Collections.Generic;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class InteractionAura : MonoBehaviour
    {
        private List<IDoorInteractable> doorsInTrigger = new ();
        private List<ISwitchInteractable> switchesInTrigger = new ();

        public IReadOnlyList<IDoorInteractable> DoorsInTrigger => doorsInTrigger;
        public IReadOnlyList<ISwitchInteractable> SwitchesInTrigger => switchesInTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDoorInteractable>(out var door))
            {
                doorsInTrigger.Add(door);
            }
            if (other.TryGetComponent<ISwitchInteractable>(out var switchItem))
            {
                switchesInTrigger.Add(switchItem);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDoorInteractable>(out var door))
            {
                doorsInTrigger.Remove(door);
            }
            if (other.TryGetComponent<ISwitchInteractable>(out var switchItem))
            {
                switchesInTrigger.Remove(switchItem);
            }
        }
    }
}