using System;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class GhostHuntAura : MonoBehaviour
    {
        public event Action<IGhostHuntingInteractable> InteractableStay; 
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                for (var i = 0; i < player.Inventory.Items.Count; i++)
                {
                    if (player.Inventory.Items[i] is IGhostHuntingInteractable ghostHuntingInteractable)
                    {
                        ghostHuntingInteractable.OnGhostInteractionStay();
                        InteractableStay?.Invoke(ghostHuntingInteractable);
                    }
                }
            }
            else if (other.TryGetComponent<IGhostHuntingInteractable>(out var ghostHuntingInteractable))
            {
                ghostHuntingInteractable.OnGhostInteractionStay();
                InteractableStay?.Invoke(ghostHuntingInteractable);
            }
        }
    }
}
