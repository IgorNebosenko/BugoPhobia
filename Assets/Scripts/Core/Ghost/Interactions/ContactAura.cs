using System.Collections.Generic;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class ContactAura : MonoBehaviour
    {
        private List<IPlayer> _playersInAura = new();
        
        public IReadOnlyList<IPlayer> PlayersInAura => _playersInAura;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
                _playersInAura.Add(player);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out var player))
                _playersInAura.Remove(player);
        }
    }
}