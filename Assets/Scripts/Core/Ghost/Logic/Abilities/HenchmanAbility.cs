using System;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using ElectrumGames.Extensions;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class HenchmanAbility : IGhostAbility
    {
        private readonly MissionPlayersHandler _missionPlayersHandler;
        private readonly GhostController _ghostController;
        private readonly GhostEmfZonePool _emfZonesPool;
        private readonly EmfData _emfData;
        
        private float _cooldownTime;

        private GhostConstants _ghostConstants;
        
        public bool IsInterrupt { get; set; }

        public HenchmanAbility(MissionPlayersHandler missionPlayersHandler, GhostController ghostController, 
            GhostEmfZonePool zonePool, EmfData emfData)
        {
            _missionPlayersHandler = missionPlayersHandler;
            _ghostController = ghostController;
            _emfZonesPool = zonePool;
            _emfData = emfData;
        }
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
            var player = _missionPlayersHandler.Players.PickRandom();

            _ghostController.transform.position = player.Position;
            
            var emfZone = _emfZonesPool.SpawnCylinderZone(null, _emfData.OtherInteractionHeightOffset,
                _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.GetEmfOtherInteract());
            emfZone.transform.position = player.Position;
            Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
            
            return true;
        }
    }
}