using System.Collections.Generic;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Items.GhostWritable;
using ElectrumGames.Extensions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items.Zones.Active
{
    public class GhostWritingZone : MonoBehaviour
    {
        private List<GhostWritableBase> _writables = new();

        private GhostWritableConfig _config;
        private GhostEmfZonePool _emfZonePool;
        private bool _hasEvidence;
        private bool _hasEmf5;
        private EmfData _data;

        private float _cooldown;

        [Inject] //for tests
        private void Construct(GhostWritableConfig config, GhostEmfZonePool emfZonePool, EmfData emfData)
        {
            _config = config;
            _emfZonePool = emfZonePool;
            _hasEmf5 = true;
            _hasEvidence = true;
            _data = emfData;
        }

        public void Init(GhostWritableConfig config, GhostEmfZonePool emfZonePool, bool hasEvidence, bool hasEmf5, 
            EmfData emfData)
        {
            _config = config;
            _emfZonePool = emfZonePool;
            _hasEvidence = hasEvidence;
            _hasEmf5 = hasEmf5;
            _data = emfData;
        }

        private void Update()
        {
            if (!_hasEvidence)
                return;

            _cooldown += Time.deltaTime;
            
            if (_cooldown <= _config.WriteCooldown)
                return;

            _cooldown = 0f;
            
            if (_writables.Count == 0)
                return;

            var item = _writables.PickRandom();
            
            if (!item.UnityNullCheck() && Random.Range(0f, 1f) < item.ChanceWrite)
                Write(item);
                
        }

        private void Write(GhostWritableBase writable)
        {
            writable.Write();

            var emfLevel = _hasEmf5
                ? _data.ChanceEvidence < Random.Range(0f, 1f) ? _data.EvidenceLevel : _data.GhostWritingDefaultEmf
                : _data.GhostWritingDefaultEmf;

            _emfZonePool.SpawnCylinderZone(writable.transform, _data.GhostWritingHeightOffset,
                _data.GhostWritingCylinderSize, emfLevel);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<GhostWritableBase>(out var writiable))
            {
                _writables.Add(writiable);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GhostWritableBase>(out var writiable))
            {
                _writables.Remove(writiable);
            }
        }
    }
}