using System;
using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Extensions;
using ElectrumGames.GlobalEnums;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public abstract class BaseNonHuntLogic : INonHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        private readonly GhostActivityData _ghostActivityData;
        private readonly GhostEmfZonePool _emfZonesPool;
        private readonly EmfData _emfData;
        
        private const float DistanceTolerance = 0.1f;
        
        private GhostVariables _ghostVariables;
        private GhostConstants _ghostConstants;
        private int _roomId;

        private float _doorInteractionTime;
        private float _switchesInteractionTime;
        private float _thrownInteractionTime;
        private float _otherInteractionTime;
        
        private bool _isMoving;
        private float _stayTime;

        protected virtual float DoorsInteractions => _ghostVariables.doorsInteractions;
        protected virtual float SwitchesInteractions => _ghostVariables.switchesInteractions;
        protected virtual float ThrowsInteractions => _ghostVariables.throws;
        protected virtual float OtherInteractions => _ghostVariables.otherInteractions;
        
        public bool IsInterrupt { get; set; }

        public BaseNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = ghostDifficultyData;
            _ghostActivityData = activityData;
            _emfZonesPool = emfZonesPool;
            _emfData = emfData;
        }
        
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostVariables = variables;
            _ghostConstants = constants;
            _roomId = roomId;
            
            _ghostController.InteractionAura.gameObject.SetActive(true);
        }

        public void FixedSimulate()
        {
            TryInteract();
            
            MoveProcess();
        }
        
        public void ForceInteract()
        {
            _doorInteractionTime = Mathf.Infinity;
            _switchesInteractionTime = Mathf.Infinity;
            _thrownInteractionTime = Mathf.Infinity;
            _otherInteractionTime = Mathf.Infinity;
            
            TryInteract();
            
            _doorInteractionTime = 0f;
            _switchesInteractionTime = 0f;
            _thrownInteractionTime = 0f;
            _otherInteractionTime = 0f;
        }

        protected virtual void MoveProcess()
        {
            if (IsInterrupt)
                return;
            
            if (_isMoving)
            {
                if (_ghostController.NavmeshRemainingDistance < DistanceTolerance)
                {
                    _isMoving = false;
                    _ghostController.SetSpeed(0f);
                    _ghostController.SetGhostAnimationSpeed(0f);
                }

                return;
            }

            _stayTime += Time.fixedDeltaTime;

            if (_stayTime >= _ghostDifficultyData.MovingToPointCooldown)
            {
                _stayTime = 0f;
                
                if (Random.Range(0f, 1f) > _ghostDifficultyData.MovingToPointChance)
                    return;
                
                _isMoving = true;
                MoveToPoint(GetTargetPoint());
            }
        }

        private void TryInteract()
        {
            DoorsInteract();
            SwitchesInteract();
            ThrowsInteract();
            OtherInteract();
        }

        protected virtual void DoorsInteract()
        {
            if (IsInterrupt)
                return;

            _doorInteractionTime += Time.fixedDeltaTime;

            if (_doorInteractionTime >= _ghostDifficultyData.DoorsInteractionCooldown &&
                _ghostController.InteractionAura.DoorsInTrigger is {Count: > 0})
            {
                _doorInteractionTime = 0f;

                if (Random.Range(0f, 1f) < DoorsInteractions)
                {
                    var randomDoor = _ghostController.InteractionAura.DoorsInTrigger.PickRandom();
                    
                    if (!randomDoor.DoorWithLock)
                    {
                        var emfZone = _emfZonesPool.SpawnCylinderZone(randomDoor.Transform, _emfData.DoorHeightOffset, 
                            _emfData.DoorCylinderSize, _ghostController.EvidenceController.GetEmfInteractDoor());

                        Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                            .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                        
                        randomDoor.TouchDoor(Random.Range(_ghostConstants.minDoorAngle, _ghostConstants.maxDoorAngle),
                            Random.Range(_ghostConstants.minDoorTouchTime, _ghostConstants.maxDoorTouchTime),
                            _ghostController.EvidenceController.Evidences.Contains(EvidenceType.UV));
                    }
                }
            }
        }

        protected virtual void SwitchesInteract()
        {
            if (IsInterrupt)
                return;

            _switchesInteractionTime += Time.fixedDeltaTime;

            if (_switchesInteractionTime >= _ghostDifficultyData.SwitchesInteractionCooldown &&
                _ghostController.InteractionAura.SwitchesInTrigger is {Count: > 0})
            {
                _switchesInteractionTime = 0f;

                if (Random.Range(0f, 1f) < SwitchesInteractions)
                {
                    var randomSwitch = _ghostController.InteractionAura.SwitchesInTrigger.PickRandom();

                    var emfZone = _emfZonesPool.SpawnCylinderZone(randomSwitch.Transform, _emfData.SwitchHeightOffset,
                        _emfData.SwitchCylinderSize, _ghostController.EvidenceController.GetEmfInteractSwitch());

                    Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                        .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                    
                    if (Random.Range(0f, 1f) < _ghostActivityData.ChanceOnSwitch)
                        randomSwitch.SwitchOn();
                    else
                        randomSwitch.TrySwitchOffByGhost();
                }
            }
        }

        protected virtual void ThrowsInteract()
        {
            if (IsInterrupt)
                return;

            _thrownInteractionTime += Time.fixedDeltaTime;

            if (_thrownInteractionTime >= _ghostDifficultyData.ThrowCooldown &&
                _ghostController.InteractionAura.ThrownInTrigger is {Count: > 0})
            {
                _thrownInteractionTime = 0f;
                
                if (Random.Range(0f, 1f) < ThrowsInteractions)
                {
                    var randomThrown = _ghostController.InteractionAura.ThrownInTrigger.PickRandom();

                    var emfZone = _emfZonesPool.SpawnSphereZone(randomThrown.Transform, _emfData.ThrowHeightOffset,
                        _emfData.ThrowSphereSize, _ghostController.EvidenceController.OnThrowInteract());

                    Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                        .Subscribe(_ => _emfZonesPool.DespawnSphereZone(emfZone));
                    
                    randomThrown.ThrowItem(_ghostActivityData.ThrownForce);
                }
            }
        }

        protected virtual void OtherInteract()
        {
            if (IsInterrupt)
                return;

            _otherInteractionTime += Time.fixedDeltaTime;

            if (_otherInteractionTime >= _ghostDifficultyData.OtherInteractionCooldown &&
                _ghostController.InteractionAura.OtherInteractionsInTrigger is {Count: > 0})
            {
                if (Random.Range(0f, 1f) < OtherInteractions)
                {
                    _otherInteractionTime = 0f;
                    
                    var randomOtherInteraction =
                        _ghostController.InteractionAura.OtherInteractionsInTrigger.PickRandom();
                    
                    var emfZone = _emfZonesPool.SpawnCylinderZone(randomOtherInteraction.Transform, _emfData.OtherInteractionHeightOffset,
                        _emfData.OtherInteractionCylinderSize, _ghostController.EvidenceController.GetEmfOtherInteract());

                    Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                        .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                    
                    randomOtherInteraction.Interact();
                }
            }
            else if (_otherInteractionTime >= _ghostDifficultyData.OtherInteractionCooldown &&
                     !_ghostController.InteractionAura.FuseBox.UnityNullCheck())
            {
                _otherInteractionTime = 0f;
                
                var result = _ghostController.InteractionAura.FuseBox.TryInteract(_ghostConstants.canSwitchOnLight);

                if (result)
                {
                    var emfZone = _emfZonesPool.SpawnCylinderZone(
                        _ghostController.InteractionAura.FuseBox.FuseBoxTransform,
                        _emfData.OtherInteractionHeightOffset, _emfData.OtherInteractionCylinderSize,
                        _ghostController.EvidenceController.GetEmfOtherInteract());

                    Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                        .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
                }
            }
        }

        private Vector3 GetTargetPoint()
        {
            var listData = new List<(Vector3 pos, float chance)>();
            var hashRoomsIds = new HashSet<int>();

            for (var i = 0; i < _ghostController.Rooms[_roomId].GhostRoomHandler.ActivityPoints.Count; i++)
            {
                listData.Add((_ghostController.Rooms[_roomId].GhostRoomHandler.ActivityPoints[i].position, 
                    _ghostDifficultyData.InRoomWeightPoint));
            }
            
            hashRoomsIds.Add(_roomId);

            var modifierMoving = 0f;

            //ToDO refactor this!
            switch (_ghostActivityData.DistanceMoving)
            {
                case DistanceMoving.Minimal:
                    modifierMoving = 0f;
                    break;
                case DistanceMoving.Low:
                    modifierMoving = 0.33f;
                    break;
                case DistanceMoving.Medium:
                    modifierMoving = 0.66f;
                    break;
                case DistanceMoving.High:
                    modifierMoving = 1f;
                    break;
                case DistanceMoving.Extreme:
                    modifierMoving = 3f;
                    break;
            }

            if (_ghostActivityData.DistanceMoving != DistanceMoving.Minimal)
            {
                for (var i = 0; i < _ghostController.Rooms[_roomId].NeighborRooms.Count; i++)
                {
                    for (var j = 0;
                         j < _ghostController.Rooms[_roomId].NeighborRooms[i].GhostRoomHandler.ActivityPoints.Count;
                         j++)
                    {
                        listData.Add((
                            _ghostController.Rooms[_roomId].NeighborRooms[i].GhostRoomHandler.ActivityPoints[j]
                                .position,
                            _ghostDifficultyData.NeighborRoomWeightPoint * modifierMoving));
                    }

                    hashRoomsIds.Add(_ghostController.Rooms[_roomId].NeighborRooms[i].RoomId);
                }
            }

            if (_ghostActivityData.DistanceMoving is DistanceMoving.Extreme or DistanceMoving.High)
            {
                for (var i = 0; i < _ghostController.Rooms.Count; i++)
                {
                    if (hashRoomsIds.Contains(i))
                        continue;

                    for (var j = 0; j < _ghostController.Rooms[i].GhostRoomHandler.ActivityPoints.Count; j++)
                    {
                        listData.Add((_ghostController.Rooms[i].GhostRoomHandler.ActivityPoints[j].position, 
                                _ghostDifficultyData.OtherWeightPoint * modifierMoving));
                    }
                }
            }


            return listData.PickRandomByWeight(x => x.chance).pos;
        }

        public void MoveToPoint(Vector3 point)
        {
            _ghostController.SetSpeed(_ghostActivityData.DefaultNonHuntSpeed);
            _ghostController.SetGhostAnimationSpeed(_ghostActivityData.DefaultNonHuntSpeed /
                                                    _ghostActivityData.MaxGhostSpeed);
            _ghostController.MoveTo(point);
        }
    }
}