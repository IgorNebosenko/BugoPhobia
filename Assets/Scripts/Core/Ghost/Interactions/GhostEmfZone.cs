using System.Collections.Generic;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class GhostEmfZone : MonoBehaviour
    {
        [field: SerializeField] public int EmfLevel { get; private set; }

        private List<ItemEmfElectronic> _cashedItems = new();

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
                        if (!_cashedItems.Contains(emf))
                            _cashedItems.Add(emf);
                    }
                }
            }
            else if (other.TryGetComponent<ItemEmfElectronic>(out var emf))
            {
                emf.SetEmfLevel(EmfLevel);
                if (!_cashedItems.Contains(emf))
                    _cashedItems.Add(emf);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                for (var i = 0; i < player.Inventory.Items.Count; i++)
                {
                    if (player.Inventory.Items[i] is ItemEmfElectronic emf)
                    {
                        emf.SetEmfLevel(0);
                        _cashedItems.Remove(emf);
                    }
                }
            }
            else if (other.TryGetComponent<ItemEmfElectronic>(out var emf))
            {
                emf.SetEmfLevel(0);
                _cashedItems.Remove(emf);
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < _cashedItems.Count; i++)
            {
                _cashedItems[i].SetEmfLevel(0);
            }
            _cashedItems.Clear();
        }
    }
}