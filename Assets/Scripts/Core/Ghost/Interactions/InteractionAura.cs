using System.Collections.Generic;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class InteractionAura : MonoBehaviour
    {
        public IReadOnlyList<IDoorInteractable> DoorsInTrigger { get; private set; }
        
        public void OnTriggerStay(Collider other)
        {
            DoorsInTrigger = other.GetComponents<IDoorInteractable>();
        }
    }
}