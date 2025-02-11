using System;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Missions;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic
{
    public class FuseBoxCounterLogic
    {
        private readonly IFuseBoxInteractable _fuseBox;
        private readonly float _chance;
        private readonly float _cooldown;
        private readonly GhostEmfZonePool _emfZonesPool;
        private readonly EmfData _emfData;
        private readonly GhostController _ghostController;

        private float _time;
        
        public FuseBoxCounterLogic(IFuseBoxInteractable fuseBox, float chance, float cooldown, 
            GhostEmfZonePool emfZonePool, EmfData emfData, GhostController ghostController)
        {
            _fuseBox = fuseBox;
            _chance = chance;
            _cooldown = cooldown;
            _emfZonesPool = emfZonePool;
            _emfData = emfData;
            _ghostController = ghostController;
        }
        
        public void FixedSimulate()
        {
            _time += Time.fixedDeltaTime;

            if (_time >= _cooldown)
            {
                _time = 0f;

                if (Random.Range(0f, 1f) <= _chance)
                {
                    _fuseBox.ShutDown();
                    
                    var emfZone = _emfZonesPool.SpawnCylinderZone(_fuseBox.FuseBoxTransform, _emfData.OtherInteractionHeightOffset,
                        _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.GetEmfOtherInteract());

                    Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                        .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                }
            }
        }
    }
}