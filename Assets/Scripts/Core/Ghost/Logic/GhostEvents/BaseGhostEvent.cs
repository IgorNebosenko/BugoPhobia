using System;
using ElectrumGames.Configs;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
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
        
        public bool IsInterrupt { get; set; }

        public BaseGhostEvent(GhostController ghostController, GhostDifficultyData difficultyData, 
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = difficultyData;
            _ghostActivityData = activityData;
            _emfZonesPool = emfZonesPool;
            _emfData = emfData;
        }
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostVariables = variables;
            _ghostConstants = constants;
            _roomId = roomId;
            
            _ghostController.GhostEventAura.gameObject.SetActive(true);
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;

            _ghostEventTime += Time.fixedDeltaTime;
            
            if (_ghostEventTime >= _ghostConstants.ghostEventCooldown * _ghostDifficultyData.GhostEventsCooldownModifier
                && CheckIsPlayerNear())
            {
                _ghostEventTime = 0f;

                if (Random.Range(0f, 1f) < _ghostVariables.ghostEvents)
                {
                    var ghostEventType = SelectGhostEventType();

                    switch (ghostEventType)
                    {
                        case GhostEventType.Appear:
                            GhostEventAppear(SelectAppearType(), 
                                Random.Range(0f, 1f) < _ghostDifficultyData.RedLightChance);
                            break;
                        case GhostEventType.Chase:
                            GhostChasePlayer(_player, IsGhostByCloud());
                            break;
                        case GhostEventType.Singing:
                            GhostSingingEvent(SelectAppearType());
                            break;
                        case GhostEventType.AppearThanChase:
                            AppearThanChasePlayer(_player);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            if (_isGhostEvent)
            {
                AppearInterference();
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
        

        public bool CheckIsPlayerNear()
        {
            //ToDo is in one room && roomId == current room
            return _ghostController.GhostEventAura.PlayersInAura.Count > 0;
        }
        

        protected virtual void GhostEventAppear(GhostAppearType appearType, bool redLight)
        {
            StopGhostEvent();

            Debug.Log("Start Ghost Appear");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);

            var targetPlayer = _ghostController.GhostEventAura.PlayersInAura.PickRandom();

            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.IsStopped(true);
            _ghostController.transform.LookAt(targetPlayer.Position);
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinStayGhostEventTime,
                        _ghostDifficultyData.MaxStayGhostEventTime)))
                .Subscribe(
                    _ =>
                    {
                        StopGhostEvent();
                        CreateGhostEventZone();
                    });
            
        }

        protected virtual void GhostChasePlayer(IHavePosition player, bool isByCloud)
        {
            StopGhostEvent();

            Debug.Log("Start Ghost Chase");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);
            
            var targetPlayer = _ghostController.GhostEventAura.PlayersInAura.PickRandom();
            
            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.transform.LookAt(targetPlayer.Position);

            MoveToPoint(targetPlayer.Position);

            _chaseProcess = Observable.EveryFixedUpdate()
                .Subscribe(_ => MoveToPoint(targetPlayer.Position));
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinChaseGhostEventTime,
                        _ghostDifficultyData.MaxChaseGhostEventTime)))
                .Subscribe(
                    _ =>
                    {
                        StopGhostEvent();
                        CreateGhostEventZone();
                    });
        }

        protected virtual void GhostSingingEvent(GhostAppearType appearType)
        {
            StopGhostEvent();

            Debug.Log("Start Ghost Singing");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);

            var targetPlayer = _ghostController.GhostEventAura.PlayersInAura.PickRandom();

            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            _ghostController.IsStopped(true);
            _ghostController.transform.LookAt(targetPlayer.Position);
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinSingingGhostEventTime,
                        _ghostDifficultyData.MaxSingingGhostEventTime)))
                .Subscribe(
                    _ =>
                    {
                        StopGhostEvent();
                        CreateGhostEventZone();
                    });
        }

        protected virtual void AppearThanChasePlayer(IHavePosition player)
        {
            StopGhostEvent();

            Debug.Log("Start Ghost Appear Than Chase");
            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);

            var targetPlayer = _ghostController.GhostEventAura.PlayersInAura.PickRandom();
            
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
                        .Subscribe(
                            _ =>
                            {
                                StopGhostEvent();
                                CreateGhostEventZone();
                            });
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