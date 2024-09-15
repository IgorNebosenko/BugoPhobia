using ElectrumGames.Core.Items;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class GhostEmfZone : MonoBehaviour
    {
        [field: SerializeField] public int EmfLevel { get; private set; }

        public void SetLevel(int level)
        {
            EmfLevel = level;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                for (var i = 0; i < player.Inventory.Items.Count; i++)
                {
                    if (player.Inventory.Items[i] is ItemEmfElectronic emf)
                    {
                        emf.SetEmfLevel(EmfLevel);
                    }
                }
            }
            else if (other.TryGetComponent<ItemEmfElectronic>(out var emf))
            {
                emf.SetEmfLevel(EmfLevel);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"OnTriggerStay: {gameObject.name} {other.name}");
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                for (var i = 0; i < player.Inventory.Items.Count; i++)
                {
                    if (player.Inventory.Items[i] is ItemEmfElectronic emf)
                    {
                        emf.SetEmfLevel(0);
                    }
                }
            }
            else if (other.TryGetComponent<ItemEmfElectronic>(out var emf))
            {
                emf.SetEmfLevel(0);
            }
        }
    }
}