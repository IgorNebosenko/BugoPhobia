using System.Collections.Generic;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Player;
using NUnit.Framework;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class GhostAppearAura : MonoBehaviour
    {
        private List<IPlayer> _playersInAura = new();
        private List<IPlayer> _playersOutOfAura = new();
        
        private List<IStartHuntInteractable> _startHuntInteractableList = new();
        
        private List<IGhostHuntingInteractableStay> _ghostHuntingInteractableStay = new();
        private List<IGhostHuntingInteractableExit> _ghostHuntingInteractableExit = new();
        
        public IReadOnlyList<IPlayer> PlayersInAura => _playersInAura;
        public IReadOnlyList<IPlayer> PlayersOutOfAura => _playersOutOfAura;
        
        public IReadOnlyList<IStartHuntInteractable> StartHuntInteractableList => _startHuntInteractableList;

        public IReadOnlyList<IGhostHuntingInteractableStay> GhostHuntingInteractableStay =>
            _ghostHuntingInteractableStay;

        public IReadOnlyList<IGhostHuntingInteractableExit> GhostHuntingInteractableExit =>
            _ghostHuntingInteractableExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
                _playersInAura.Add(player);
            if (other.TryGetComponent<IStartHuntInteractable>(out var startHuntInteractable))
                _startHuntInteractableList.Add(startHuntInteractable);
            
            if (other.TryGetComponent<IGhostHuntingInteractableStay>(out var ghostHuntingInteractableStay))
                _ghostHuntingInteractableStay.Add(ghostHuntingInteractableStay);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
            {
                _playersInAura.Remove(player);
                _playersOutOfAura.Add(player);
            }

            if (other.TryGetComponent<IStartHuntInteractable>(out var startHuntInteractable))
                _startHuntInteractableList.Remove(startHuntInteractable);
            
            if (other.TryGetComponent<IGhostHuntingInteractableStay>(out var ghostHuntingInteractableStay))
                _ghostHuntingInteractableStay.Remove(ghostHuntingInteractableStay);
            if (other.TryGetComponent<IGhostHuntingInteractableExit>(out var ghostHuntingInteractableExit))
                _ghostHuntingInteractableExit.Add(ghostHuntingInteractableExit);
        }

        public void ResetGhostInteractableExit()
        {
            _ghostHuntingInteractableExit.Clear();
        }

        public void ResetPlayerExit()
        {
            _playersOutOfAura.Clear();
        }
    }
}