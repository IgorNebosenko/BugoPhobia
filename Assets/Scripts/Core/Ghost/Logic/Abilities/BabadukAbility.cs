using System;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Extensions;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class BabadukAbility : IGhostAbility
    {
        private readonly GhostController _ghostController;
        private readonly GhostEmfZonePool _emfZonesPool;
        private readonly GhostActivityData _ghostActivityData;
        private readonly EmfData _emfData;
        
        private GhostConstants _ghostConstants;

        private float _cooldownTime;
        
        public BabadukAbility(GhostController ghostController, GhostEmfZonePool emfZonesPool, 
            GhostActivityData ghostActivityData, EmfData emfData)
        {
            _ghostController = ghostController;
            _emfZonesPool = emfZonesPool;
            _ghostActivityData = ghostActivityData;
            _emfData = emfData;
        }
        
        public bool IsInterrupt { get; set; }
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostConstants = constants;
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;

            _cooldownTime += Time.fixedDeltaTime;

            if (_cooldownTime >= _ghostConstants.abilityCooldown)
            {
                _cooldownTime = 0f;

                if (Random.Range(0f, 1f) < _ghostConstants.abilityChance)
                {
                    TryUseAbility();
                }
            }
        }

        public bool TryUseAbility()
        {
            return ThrowsInteract() || DoorsInteract() || SwitchesInteract() || OtherInteract();
        }

        private bool ThrowsInteract()
        {
            if (_ghostController.InteractionAura.ThrownInTrigger is {Count: > 0})
            {
                var randomThrown = _ghostController.InteractionAura.ThrownInTrigger.PickRandom();
                randomThrown.ThrowItem(_ghostActivityData.ThrownForce);
                
                var emfZone = _emfZonesPool.SpawnCylinderZone(null, _emfData.OtherInteractionHeightOffset,
                    _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.OnThrowInteract());
                emfZone.transform.position = _ghostController.transform.position;
                Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                    .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                
                return true;
            }
            
            return false;
        }

        private bool DoorsInteract()
        {
            if (_ghostController.InteractionAura.DoorsInTrigger is {Count: > 0})
            {
                var randomDoor = _ghostController.InteractionAura.DoorsInTrigger.PickRandom();
                randomDoor.TouchDoor(Random.Range(_ghostConstants.minDoorAngle, _ghostConstants.maxDoorAngle),
                    Random.Range(_ghostConstants.minDoorTouchTime, _ghostConstants.maxDoorTouchTime));
                
                var emfZone = _emfZonesPool.SpawnCylinderZone(null, _emfData.OtherInteractionHeightOffset,
                    _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.GetEmfInteractDoor());
                emfZone.transform.position = _ghostController.transform.position;
                Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                    .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                
                return true;
            }
            
            return false;
        }

        private bool SwitchesInteract()
        {
            if (_ghostController.InteractionAura.SwitchesInTrigger is {Count: > 0})
            {
                var randomSwitch = _ghostController.InteractionAura.SwitchesInTrigger.PickRandom();
                
                if (Random.Range(0f, 1f) < _ghostActivityData.ChanceOnSwitch)
                    randomSwitch.SwitchOn();
                else
                    randomSwitch.TrySwitchOffByGhost();
                
                var emfZone = _emfZonesPool.SpawnCylinderZone(null, _emfData.OtherInteractionHeightOffset,
                    _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.GetEmfInteractSwitch());
                emfZone.transform.position = _ghostController.transform.position;
                Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                    .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                
                return true;
            }
            
            return false;
        }

        private bool OtherInteract()
        {
            if (_ghostController.InteractionAura.OtherInteractionsInTrigger is {Count: > 0})
            {
                var randomOtherInteract = _ghostController.InteractionAura.OtherInteractionsInTrigger.PickRandom();
                randomOtherInteract.Interact();
                
                var emfZone = _emfZonesPool.SpawnCylinderZone(null, _emfData.OtherInteractionHeightOffset,
                    _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.GetEmfOtherInteract());
                emfZone.transform.position = _ghostController.transform.position;
                Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                    .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                
                return true;
            }
            
            return false;
        }
    }
}