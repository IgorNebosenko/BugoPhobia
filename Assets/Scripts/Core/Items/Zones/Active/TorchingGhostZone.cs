using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Items.Equipment.Torchable;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones.Active
{
    public class TorchingGhostZone : MonoBehaviour
    {
        private TorchConfig _torchConfig;
        private GhostEmfZonePool _ghostEmfZonePool;
        private bool _hasEvidence;
        private bool _hasEmf5;
        private EmfData _emfData;

        private float _cooldown;

        public void Init(TorchConfig torchConfig, GhostEmfZonePool emfZonePool, bool hasEvidence, bool hasEmf5, EmfData data)
        {
            _torchConfig = torchConfig;
            _ghostEmfZonePool = emfZonePool;
            _hasEvidence = hasEvidence;
            _hasEmf5 = hasEmf5;
            _emfData = data;
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
            {
                torchable.Torch();

                var emfLevel = _hasEmf5 ? _emfData.ChanceEvidence < Random.Range(0f, 1f) ? 4 : _emfData.TorchDefaultEmf 
                    : _emfData.TorchDefaultEmf;

                _ghostEmfZonePool.SpawnSphereZone(torchable.transform, _emfData.TorchHeightOffset,
                    _emfData.TorchSphereSize, emfLevel);
            }
        }
    }
}