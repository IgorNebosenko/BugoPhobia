using System;
using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions.CommonInterfaces;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public abstract class BaseGhostEvent : IGhostEventLogic
    {
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        private readonly GhostActivityData _ghostActivityData;
        private readonly GhostEmfZonePool _emfZonesPool;
        private readonly EmfData _emfData;
        private readonly MissionPlayersHandler _missionPlayersHandler;
        
        private GhostVariables _ghostVariables;
        private GhostConstants _ghostConstants;
        private int _roomId;
        
        private IDisposable _ghostEventDisposable;
        private IDisposable _chaseProcess;
        private IDisposable _appearAndChaseProcess;
        
        private IHavePosition _player;
        private Room _currentRoom;

        private float _ghostEventTime;
        private bool _isGhostEvent;

        private bool _isDecreaseSanityByTouch;
        
        public bool IsInterrupt { get; set; }

        public BaseGhostEvent(GhostController ghostController, GhostDifficultyData difficultyData, 
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData, MissionPlayersHandler missionPlayersHandler)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = difficultyData;
            _ghostActivityData = activityData;
            _emfZonesPool = emfZonesPool;
            _emfData = emfData;
            _missionPlayersHandler = missionPlayersHandler;
        }
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostVariables = variables;
            _ghostConstants = constants;
            _roomId = roomId;
            
            _ghostController.GhostEventAura.gameObject.SetActive(true);
            _ghostController.ContactAura.gameObject.SetActive(true);
        }

        public void FixedSimulate()
        {
            if (IsInterrupt || !_missionPlayersHandler.IsAnyPlayerInHouse)
            {
                if (_isGhostEvent)
                    StopGhostEvent();
                
                return;
            }

            _ghostEventTime += Time.fixedDeltaTime;
            
            if (_ghostEventTime >= _ghostConstants.ghostEventCooldown * _ghostDifficultyData.GhostEventsCooldownModifier)
            {
                var nearPlayer = GetNearPlayer();
                if (nearPlayer != null)
                {
                    _ghostEventTime = 0f;

                    if (Random.Range(0f, 1f) < _ghostVariables.ghostEvents)
                    {
                        var ghostRoom = _ghostController.GetCurrentRoom();
                    
                        if (Random.Range(0f, 1f) < _ghostDifficultyData.DoorcCloseChance)
                            ghostRoom.DoorsRoomHandler.CloseDoors();
                        
                        ghostRoom.LightRoomHandler.SwitchOffLight();
                        
                        var ghostEventType = SelectGhostEventType();

                        switch (ghostEventType)
                        {
                            case GhostEventType.Appear:
                                GhostEventAppear(nearPlayer, SelectAppearType(), ghostRoom);
                                break;
                            case GhostEventType.Chase:
                                GhostChasePlayer(nearPlayer, IsGhostByCloud());
                                break;
                            case GhostEventType.Singing:
                                GhostSingingEvent(nearPlayer, SelectAppearType());
                                break;
                            case GhostEventType.AppearThanChase:
                                AppearThanChasePlayer(nearPlayer);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            if (_isGhostEvent)
            {
                AppearInterference();

                if (_ghostController.ContactAura.PlayersInAura.Count > 0)
                {
                    if (_isDecreaseSanityByTouch)
                    {
                        _ghostController.ContactAura.PlayersInAura[0].Sanity.GetGhostEvent(
                            _ghostDifficultyData.MinGhostEventDrainSanity,
                            _ghostDifficultyData.MaxGhostEventDrainSanity, _ghostController.NetId);
                    }

                    _isDecreaseSanityByTouch = false;
                    
                    OnGhostEventEnded(0);
                }
            }
        }

        protected void CreateGhostEventZone()
        {
            var emfZone = _emfZonesPool.SpawnCylinderZone(_ghostController.transform.position, _emfData.GhostEventHeightOffset, 
                _emfData.GhostEventCylinderSize, _emfData.GhostEventDefaultEmf);
                
            Observable.Timer(TimeSpan.FromSeconds(_emfData.TimeEmfInteraction))
                .Subscribe(_ => _emfZonesPool.DespawnCylinderZone(emfZone));
        }

        protected virtual GhostEventType SelectGhostEventType()
        {
            return (GhostEventType) Random.Range(0, (int) GhostEventType.AppearThanChase);
        }

        protected virtual GhostAppearType SelectAppearType()
        {
            return (GhostAppearType) Random.Range(0, (int) GhostAppearType.Transparent);
        }

        protected virtual bool IsGhostByCloud()
        {
            return Random.Range(0, 2) != 0;
        }
        

        public IPlayer GetNearPlayer()
        {
            if (_ghostController.GhostEventAura.PlayersInAura.Count == 0)
                return null;
            
            var ghostRoom = _ghostController.GetCurrentRoom();
            var listAvailableIds = ghostRoom.NeighborRooms.Select(x => x.RoomId).ToList();
            listAvailableIds.Add(ghostRoom.RoomId);

            for (var i = 0; i < _ghostController.GhostEventAura.PlayersInAura.Count; i++)
            {
                var playerRoomId = _ghostController.GhostEventAura.PlayersInAura[i].GetCurrentStayRoom();
                if (listAvailableIds.Any(x => x == playerRoomId))
                    return _ghostController.GhostEventAura.PlayersInAura[i];
            }

            return null;
        }
        

        protected virtual void GhostEventAppear(IPlayer targetPlayer, GhostAppearType appearType, Room ghostRoom)
        {
            StopGhostEvent();
            
            targetPlayer.Sanity.GetGhostEvent(
                _ghostDifficultyData.MinGhostEventDrainSanity,
                _ghostDifficultyData.MaxGhostEventDrainSanity, _ghostController.NetId);
            
            if (Random.Range(0f, 1f) < _ghostDifficultyData.RedLightChance)
            {
                ghostRoom.LightRoomHandler.RedLight = true;
                ghostRoom.LightRoomHandler.SwitchOnLight();
            }

            Debug.Log("Start Ghost Appear");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);

            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.IsStopped(true);
            _ghostController.transform.LookAt(targetPlayer.Position);
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinStayGhostEventTime,
                        _ghostDifficultyData.MaxStayGhostEventTime)))
                .Subscribe(OnGhostEventEnded);
            
        }

        protected virtual void GhostChasePlayer(IPlayer targetPlayer, bool isByCloud)
        {
            StopGhostEvent();

            _isDecreaseSanityByTouch = true;
            
            Debug.Log("Start Ghost Chase");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);
            
            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.transform.LookAt(targetPlayer.Position);

            MoveToPoint(targetPlayer.Position);

            _chaseProcess = Observable.EveryFixedUpdate()
                .Subscribe(_ => MoveToPoint(targetPlayer.Position));
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinChaseGhostEventTime,
                        _ghostDifficultyData.MaxChaseGhostEventTime)))
                .Subscribe(OnGhostEventEnded);
        }

        protected virtual void GhostSingingEvent(IPlayer targetPlayer, GhostAppearType appearType)
        {
            StopGhostEvent();
            
            targetPlayer.Sanity.GetGhostEvent(
                _ghostDifficultyData.MinGhostEventDrainSanity,
                _ghostDifficultyData.MaxGhostEventDrainSanity, _ghostController.NetId);

            Debug.Log("Start Ghost Singing");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);

            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.IsStopped(true);
            _ghostController.transform.LookAt(targetPlayer.Position);
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinSingingGhostEventTime,
                        _ghostDifficultyData.MaxSingingGhostEventTime)))
                .Subscribe(OnGhostEventEnded);
        }

        protected virtual void AppearThanChasePlayer(IPlayer targetPlayer)
        {
            StopGhostEvent();

            _isDecreaseSanityByTouch = true;

            Debug.Log("Start Ghost Appear Than Chase");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);
            
            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.IsStopped(true);
            _ghostController.transform.LookAt(targetPlayer.Position);

            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(_ghostDifficultyData.SafeHuntTime)).Subscribe(
                _ =>
                {
                    _ghostController.IsStopped(false);

                    _chaseProcess = Observable.EveryUpdate()
                        .Subscribe(_ => MoveToPoint(targetPlayer.Position));
                    
                    _appearAndChaseProcess = Observable.Timer(TimeSpan.FromSeconds(
                            Random.Range(_ghostDifficultyData.MinSingingGhostEventTime,
                                _ghostDifficultyData.MaxSingingGhostEventTime)))
                        .Subscribe(OnGhostEventEnded);
                });
        }

        protected virtual void AppearInterference()
        {
            for (var i = 0; i < _ghostController.GhostEventAura.PlayersInAura.Count; i++)
            {
                for (var j = 0; j < _ghostController.GhostEventAura.PlayersInAura[i].Inventory.Items.Count; j++)
                {
                    if (_ghostController.GhostEventAura.PlayersInAura[i].Inventory.Items[j]
                        is IGhostHuntingInteractableStay ghostHuntingInteractableStay)
                    {
                        ghostHuntingInteractableStay.OnGhostInteractionStay();
                    }
                }
            }
                
            for (var i = 0; i < _ghostController.GhostEventAura.GhostHuntingInteractableStay.Count; i++)
            {
                _ghostController.GhostEventAura.GhostHuntingInteractableStay[i].OnGhostInteractionStay();
            }

            if (_ghostController.GhostEventAura.GhostHuntingInteractableExit.Count > 0)
            {
                for (var i = 0; i < _ghostController.GhostEventAura.GhostHuntingInteractableExit.Count; i++)
                {
                    _ghostController.GhostEventAura.GhostHuntingInteractableExit[i].OnGhostInteractionExit();
                }

                _ghostController.GhostEventAura.ResetGhostInteractableExit();
            }
        }

        protected virtual void OnGhostEventEnded(long _)
        {
            StopGhostEvent();
            CreateGhostEventZone();
                                
            var ghostRoom = _ghostController.GetCurrentRoom();
            ghostRoom.LightRoomHandler.RedLight = false;
            ghostRoom.LightRoomHandler.SwitchOffLight();
        }

        protected void StopGhostEvent()
        {
            Debug.Log("Stop Ghost Event");
            _isGhostEvent = false;
            _ghostController.IsStopped(false);
            _ghostController.SetGhostVisibility(false);
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
            
            _chaseProcess?.Dispose();
            _ghostEventDisposable?.Dispose();
            _appearAndChaseProcess?.Dispose();
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