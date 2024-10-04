using System;
using ElectrumGames.Configs;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
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
        
        private GhostVariables _ghostVariables;
        private GhostConstants _ghostConstants;
        private int _roomId;
        
        private IDisposable _ghostEventDisposable;
        
        private IHavePosition _player;
        private Room _currentRoom;

        private float _ghostEventTime;
        private bool _isGhostEvent;
        
        public bool IsInterrupt { get; set; }

        public BaseGhostEvent(GhostController ghostController, GhostDifficultyData difficultyData)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = difficultyData;
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

            Debug.Log(_ghostConstants.ghostEventCooldown * _ghostDifficultyData.GhostEventsCooldownModifier);
            
            if (_ghostEventTime >= _ghostConstants.ghostEventCooldown * _ghostDifficultyData.GhostEventsCooldownModifier
                && CheckIsPlayerNear())
            {
                _ghostEventTime = 0f;

                if (Random.Range(0f, 1f) < _ghostVariables.ghostEvents)
                {
                    if (Random.Range(0, 2) != 0)
                        GhostEventAppear((GhostAppearType)Random.Range(0, 3), Random.Range(0, 2) != 0);
                    else
                        GhostChasePlayer(_player, Random.Range(0, 2) != 0);
                }
            }

            if (_isGhostEvent)
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
        }

        public bool CheckIsPlayerNear()
        {
            return _ghostController.GhostEventAura.PlayersInAura.Count > 0;
        }

        protected virtual void GhostEventAppear(GhostAppearType appearType, bool redLight)
        {
            StopGhostEvent();

            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);

            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type
            
            _ghostEventDisposable = Observable.Timer(TimeSpan.FromSeconds(
                    Random.Range(_ghostDifficultyData.MinGhostEventTime,
                        _ghostDifficultyData.MaxGhostEventTime)))
                .Subscribe(
                    _ =>
                    {
                        _ghostController.SetGhostVisibility(false);
                        StopGhostEvent();
                    });
            
        }

        protected virtual void GhostChasePlayer(IHavePosition player, bool isByCloud)
        {
            StopGhostEvent();

            _isGhostEvent = true;
            _ghostController.SetEnabledLogic(GhostLogicSelector.GhostEvent);
        }

        protected void StopGhostEvent()
        {
            _isGhostEvent = false;
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
            
            _ghostEventDisposable?.Dispose();
        }
    }
}