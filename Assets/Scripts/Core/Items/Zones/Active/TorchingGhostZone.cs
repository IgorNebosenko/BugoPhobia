using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items.Equipment.Torchable;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones.Active
{
    public class TorchingGhostZone : MonoBehaviour
    {
        private TorchConfig _torchConfig;
        private bool _hasEvidence;

        private float _cooldown;

        public void Init(TorchConfig torchConfig, bool hasEvidence)
        {
            _torchConfig = torchConfig;
            _hasEvidence = hasEvidence;
        }

        private void FixedUpdate()
        {
            if (!_hasEvidence)
                return;

            _cooldown += Time.fixedDeltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_hasEvidence || _cooldown < _torchConfig.TorchCooldown)
                return;

            _cooldown = 0f;
            
            if (other.TryGetComponent<TorchableBase>(out var torchable))
            {
                TryTorch(torchable);
            }
            else if (other.TryGetComponent<PlayerBase>(out var player))
            {
                var currentItem = player.Inventory.Items[player.InventoryIndexHandler.CurrentIndex];
                
                if (currentItem is TorchableBase torchableBase)
                    TryTorch(torchableBase);
            }
        }

        private void TryTorch(TorchableBase torchable)
        {
            if (Random.Range(0f, 1f) < torchable.ChanceTorch)
                torchable.Torch();
        }
    }
}