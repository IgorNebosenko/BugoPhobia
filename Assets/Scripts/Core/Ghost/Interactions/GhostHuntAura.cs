using System;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class GhostHuntAura : MonoBehaviour
    {
        public event Action<IGhostHuntingInteractableStay> InteractableStay;
        public event Action<IGhostHuntingInteractableExit> InteractableExit; 
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                for (var i = 0; i < player.Inventory.Items.Count; i++)
                {
                    if (player.Inventory.Items[i] is IGhostHuntingInteractableStay ghostHuntingInteractable)
                    {
                        ghostHuntingInteractable.OnGhostInteractionStay();
                        InteractableStay?.Invoke(ghostHuntingInteractable);
                    }
                }
            }
            else if (other.TryGetComponent<IGhostHuntingInteractableStay>(out var ghostHuntingInteractable))
            {
                ghostHuntingInteractable.OnGhostInteractionStay();
                InteractableStay?.Invoke(ghostHuntingInteractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                for (var i = 0; i < player.Inventory.Items.Count; i++)
                {
                    if (player.Inventory.Items[i] is IGhostHuntingInteractableExit ghostHuntingInteractable)
                    {
                        ghostHuntingInteractable.OnGhostInteractionExit();
                        InteractableExit?.Invoke(ghostHuntingInteractable);
                    }
                }
            }
            else if (other.TryGetComponent<IGhostHuntingInteractableExit>(out var ghostHuntingInteractable))
            {
                ghostHuntingInteractable.OnGhostInteractionExit();
                InteractableExit?.Invoke(ghostHuntingInteractable);
            }
        }
    }
}
