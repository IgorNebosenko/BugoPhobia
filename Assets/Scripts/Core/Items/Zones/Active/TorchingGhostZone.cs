using System.Collections.Generic;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Items.Equipment.Torchable;
using ElectrumGames.Core.Player;
using ElectrumGames.Extensions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items.Zones.Active
{
    public class TorchingGhostZone : MonoBehaviour
    {
        private List<TorchableBase> _torchables = new ();
        
        private TorchConfig _torchConfig;
        private GhostEmfZonePool _ghostEmfZonePool;
        private bool _hasEvidence;
        private bool _hasEmf5;
        private EmfData _emfData;

        private float _cooldown;

        [Inject] // for tests
        private void Construct(TorchConfig torchConfig, GhostEmfZonePool emfZonePool, EmfData data)
        {
            _torchConfig = torchConfig;
            _ghostEmfZonePool = emfZonePool;
            _hasEvidence = true;
            _hasEmf5 = true;
            _emfData = data;
        }
        public void Init(TorchConfig torchConfig, GhostEmfZonePool emfZonePool, bool hasEvidence, bool hasEmf5, EmfData data)
        {
            _torchConfig = torchConfig;
            _ghostEmfZonePool = emfZonePool;
            _hasEvidence = hasEvidence;
            _hasEmf5 = hasEmf5;
            _emfData = data;
        }

        private void Update()
        {
            if (!_hasEvidence)
                return;

            _cooldown += Time.deltaTime;

            if (_cooldown <= _torchConfig.TorchCooldown) 
                return;
            
            _cooldown = 0f;

            if (_torchables.Count == 0)
                return;
                
            var item = _torchables.PickRandom();
                
            if (!item.UnityNullCheck() && Random.Range(0f, 1f) < item?.ChanceTorch)
                Torch(item);
        }
        
        private void Torch(TorchableBase torchable)
        {
            torchable.Torch();

            var emfLevel = _hasEmf5 ? _emfData.ChanceEvidence < Random.Range(0f, 1f) ? _emfData.EvidenceLevel : _emfData.TorchDefaultEmf 
                : _emfData.TorchDefaultEmf;
                
            _ghostEmfZonePool.SpawnSphereZone(torchable.transform, _emfData.TorchHeightOffset,
                _emfData.TorchSphereSize, emfLevel);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TorchableBase>(out var torchable))
            {
                _torchables.Add(torchable);
            }
            else if (other.TryGetComponent<PlayerBase>(out var player))
            {
                var currentItem = player.Inventory.Items[player.InventoryIndexHandler.CurrentIndex];
                
                if (currentItem is TorchableBase torchableBase)
                {
                    _torchables.Add(torchableBase);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TorchableBase>(out var torchable))
            {
                _torchables.Remove(torchable);
            }
            else if (other.TryGetComponent<PlayerBase>(out var player))
            {
                var currentItem = player.Inventory.Items[player.InventoryIndexHandler.CurrentIndex];
                
                if (currentItem is TorchableBase torchableBase)
                {
                    _torchables.Remove(torchableBase);
                }
            }
        }
    }
}