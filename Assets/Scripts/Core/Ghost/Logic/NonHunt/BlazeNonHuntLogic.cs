using System;
using System.Collections.Generic;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Extensions;
using ElectrumGames.GlobalEnums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class BlazeNonHuntLogic : INonHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        private readonly GhostActivityData _ghostActivityData;
        
        private const float DistanceTolerance = 0.1f;
        
        private GhostVariables _ghostVariables;
        private GhostConstants _ghostConstants;
        private int _roomId;

        private bool _isMoving;
        private float _stayTime;
        private Vector2 _targetPosition;
        
        public bool IsInterrupt { get; private set; }

        public BlazeNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = ghostDifficultyData;
            _ghostActivityData = activityData;
        }
        
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostVariables = variables;
            _ghostConstants = constants;
            _roomId = roomId;
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;
            
            //ToDo insert logic of interact

            if (_isMoving)
            {
                if (Vector2.Distance(new Vector2(_ghostController.transform.position.x, 
                    _ghostController.transform.position.z), _targetPosition) < DistanceTolerance)
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

            for (var i = 0; i < _ghostController.Rooms[_roomId].NeighborRooms.Count; i++)
            {
                for (var j = 0; j < _ghostController.Rooms[_roomId].NeighborRooms[i].GhostRoomHandler.ActivityPoints.Count; j++)
                {
                    listData.Add((_ghostController.Rooms[_roomId].NeighborRooms[i].GhostRoomHandler.ActivityPoints[j].position,
                            _ghostDifficultyData.NeighborRoomWeightPoint * modifierMoving));
                }

                hashRoomsIds.Add(_ghostController.Rooms[_roomId].NeighborRooms[i].RoomId);
            }

            if (_ghostActivityData.DistanceMoving == DistanceMoving.Extreme)
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
            _targetPosition = new Vector2(point.x, point.z);
            _ghostController.SetSpeed(_ghostActivityData.DefaultNonHuntSpeed);
            _ghostController.SetGhostAnimationSpeed(_ghostActivityData.DefaultNonHuntSpeed /
                                                    _ghostActivityData.MaxGhostSpeed);
            _ghostController.MoveTo(point);
        }

        public bool TryThrowItem()
        {
            return false;
        }

        public bool TryUseDoor()
        {
            return false;
        }

        public bool TrySwitchInteract()
        {
            return false;
        }

        public bool TryOtherInteraction()
        {
            return false;
        }
    }
}