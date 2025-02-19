using System;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class PoltergeistAbility : IGhostAbility
    {
        private float _cooldownTime;
        
        private GhostConstants _ghostConstants;
        private GhostVariables _ghostVariables;
        
        private readonly GhostController _ghostController;
        private readonly GhostActivityData _ghostActivityData;
        private readonly GhostEmfZonePool _emfZonesPool;
        private readonly EmfData _emfData;
        
        public bool IsInterrupt { get; set; }

        public PoltergeistAbility(GhostController ghostController, GhostActivityData activityData, 
            GhostEmfZonePool emfZonesPool, EmfData emfData)
        {
            _ghostController = ghostController;
            _ghostActivityData = activityData;
            _emfZonesPool = emfZonesPool;
            _emfData = emfData;
        }
        
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostConstants = constants;
            _ghostVariables = variables;
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;
            
            _cooldownTime += Time.fixedDeltaTime;

            if (_cooldownTime >= _ghostConstants.abilityCooldown &&
                _ghostController.InteractionAura.ThrownInTrigger is {Count: > 1})
            {
                _cooldownTime = 0f;
                if (Random.Range(0f, 1f) < _ghostConstants.abilityChance)
                {
                    for (var i = 0; i < _ghostController.InteractionAura.ThrownInTrigger.Count; i++)
                    {
                        var item = _ghostController.InteractionAura.ThrownInTrigger[i];
                        
                        var emfZone = _emfZonesPool.SpawnSphereZone(item.Transform, _emfData.ThrowHeightOffset,
                            _emfData.ThrowSphereSize, _ghostController.EvidenceController.OnThrowInteract());

                        Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                            .Subscribe(_ => _emfZonesPool.DespawnSphereZone(emfZone));
                        
                        item.ThrowItem(_ghostActivityData.ThrownForce);
                    }
                }
            }
        }

        public bool TryUseAbility()
        {
            return false;
        }
    }
}