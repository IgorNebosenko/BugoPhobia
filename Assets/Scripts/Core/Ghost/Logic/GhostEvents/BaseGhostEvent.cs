using System;
using ElectrumGames.Configs;
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
        
        public bool IsInterrupt { get; private set; }

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

            if (_ghostEventTime <= _ghostConstants.ghostEventCooldown * _ghostDifficultyData.GhostEventsCooldownModifier
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
        }

        public bool CheckIsPlayerNear()
        {
            return _ghostController.GhostEventAura.PlayersInAura.Count > 0;
        }

        protected virtual void GhostEventAppear(GhostAppearType appearType, bool redLight)
        {
            StopGhostEvent();

            _ghostController.SetGhostVisibility(true); // Todo Switch by appear type

            //Todo add lock process
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
        }

        protected void StopGhostEvent()
        {
            _ghostEventDisposable?.Dispose();
        }
    }
}