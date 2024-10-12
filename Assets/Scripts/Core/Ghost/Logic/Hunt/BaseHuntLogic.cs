using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
using ElectrumGames.GlobalEnums;
using Cysharp.Threading.Tasks;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Player;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public abstract class BaseHuntLogic : IHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        private readonly GhostActivityData _activityData;
        private readonly MissionPlayersHandler _missionPlayersHandler;
        private readonly GhostFlickConfig _ghostFlickConfig;
        private readonly HuntPoints _huntPoints;

        private GhostVariables _ghostVariables;
        private GhostConstants _ghostConstants;
        private int _roomId;

        private float _huntCooldownTime;
        private bool _isHunt;
        
        protected float huntingSpeed;
        protected bool isMoveToPlayer;

        private CancellationTokenSource _huntCancellationTokenSource;
        private CancellationTokenSource _flickCancellationTokenSource;
        private IDisposable _appearProcess;
        private IDisposable _huntProcess;
        private IDisposable _flickProcess;
        
        public bool IsInterrupt { get; set; }

        public BaseHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler, GhostFlickConfig ghostFlickConfig,
            HuntPoints huntPoints)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = ghostDifficultyData;
            _activityData = activityData;
            _missionPlayersHandler = missionPlayersHandler;
            _ghostFlickConfig = ghostFlickConfig;
            _huntPoints = huntPoints;
        }

        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostVariables = variables;
            _ghostConstants = constants;
            _roomId = roomId;
            
            _ghostController.GhostHuntAura.gameObject.SetActive(true);
        }

        public void FixedSimulate()
        {
            if (IsInterrupt || !_missionPlayersHandler.IsAnyPlayerInHouse)
            {
                if (_isHunt)
                    StopHunt();
                
                return;
            }
            
            _huntCooldownTime += Time.fixedDeltaTime;

            if (_huntCooldownTime >= _activityData.HuntCooldown)
            {
                _huntCooldownTime = 0f;

                if (CanHuntBySanity() && CanHuntByChanceHunt())
                {
                    _isHunt = true;
                    _ghostController.SetEnabledLogic(GhostLogicSelector.Hunt);
                    
                    _ghostController.SetGhostVisibility(true);
                    _ghostController.IsStopped(true);

                    _appearProcess = Observable.Timer(TimeSpan.FromSeconds(_ghostDifficultyData.SafeHuntTime))
                        .Subscribe(StartHuntProcess);
                }
            }

            if (_isHunt)
            {
                HuntInterference();
            }
        }

        protected virtual bool CanHuntBySanity()
        {
            return _missionPlayersHandler.AverageSanity <= _activityData.DefaultSanityStartHunting;
        }

        protected virtual bool CanHuntByChanceHunt()
        {
            return Random.Range(0f, 1f) < _ghostDifficultyData.HuntChance;
        }

        protected virtual void StartHuntProcess(long _)
        {
            _ghostController.IsStopped(false);

            _huntCancellationTokenSource?.Cancel();
            _huntCancellationTokenSource = new CancellationTokenSource();
            var huntToken = _huntCancellationTokenSource.Token;

            _flickCancellationTokenSource?.Cancel();
            _flickCancellationTokenSource = new CancellationTokenSource();
            var flickToken = _flickCancellationTokenSource.Token;

            Hunt(huntToken).Forget();
            Flick(flickToken).Forget();
        }

        protected virtual async UniTask Hunt(CancellationToken token)
        {
            try
            {
                var stopWatch = new Stopwatch();

                var isMoving = false;
                const float distanceTolerance = 1f;

                huntingSpeed = _ghostConstants.defaultHuntingSpeed;

                while (stopWatch.Elapsed.Seconds < _ghostDifficultyData.HuntDuration)
                {
                    if (!isMoving)
                    {
                        isMoving = true;
                        var randomPosition = _huntPoints.Positions.PickRandom().position;
                        MoveToPoint(randomPosition, false);
                    }
                    else if (_ghostController.NavmeshRemainingDistance < distanceTolerance)
                    {
                        isMoving = false;
                    }

                    ThrowItemsOnHunt();
                    TouchDoorsOnHunt();

                    CheckPlayerOnVisual();
                    CheckPlayerOnElectronic();
                    await UniTask.Delay(TimeSpan.FromMilliseconds(250), cancellationToken: token);
                    CheckPlayerOnVisual();
                    CheckPlayerOnElectronic();
                    await UniTask.Delay(TimeSpan.FromMilliseconds(250), cancellationToken: token);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Hunt was cancelled or other issue");
            }
            finally
            {
                StopHunt();
            }
        }

        protected virtual void CheckPlayerOnVisual()
        {
            if (_ghostController.GhostHuntAura.PlayersInAura is {Count: > 0})
            {
                const int layerToExclude = ~(1 << 2);
                for (var i = 0; i < _ghostController.GhostHuntAura.PlayersInAura.Count; i++)
                {
                    var directionToPlayer = _ghostController.GhostHuntAura.PlayersInAura[i].Position -
                                            _ghostController.transform.position;

                    if (IsSeePlayer(directionToPlayer, layerToExclude))
                        MoveToPoint(_ghostController.GhostHuntAura.PlayersInAura[i].Position, true);
                }
            }
        }

        protected virtual void CheckPlayerOnElectronic()
        {
        }

        protected virtual void ThrowItemsOnHunt()
        {
            if (_ghostController.InteractionAura.ThrownInTrigger is {Count: > 0})
            {
                for (var i = 0; i < _ghostController.InteractionAura.ThrownInTrigger.Count; i++)
                {
                    Debug.LogWarning("Read chance of throw during hunt from config!");
                    if (Random.Range(0f, 1f) > 0.5f)
                        continue;
                    
                    _ghostController.InteractionAura.ThrownInTrigger[i].ThrowItem(_activityData.ThrownForce);
                }
            }
                
        }

        protected virtual void TouchDoorsOnHunt()
        {
            if (_ghostController.InteractionAura.DoorsInTrigger is {Count: > 0})
            {
                for (var i = 0; i < _ghostController.InteractionAura.DoorsInTrigger.Count; i++)
                {
                    Debug.LogWarning("Read chance of door interact during hunt from config!");
                    if (Random.Range(0f, 1f) > 0.5f)
                        continue;
                    
                    _ghostController.InteractionAura.DoorsInTrigger[i].TouchDoor(
                        Random.Range(_ghostConstants.minDoorAngle, _ghostConstants.maxDoorAngle),
                        Random.Range(_ghostConstants.minDoorTouchTime, _ghostConstants.maxDoorTouchTime));
                }
            }
        }

        protected virtual async UniTask Flick(CancellationToken token)
        {
            switch (_ghostConstants.ghostVisibility)
            {
                case GhostVisibility.Invisible:
                    _ghostController.SetGhostVisibility(false);
                    break;

                case GhostVisibility.LessVisible:
                    await FlickLessVisibleAsync(token);
                    break;

                case GhostVisibility.MoreVisible:
                    await FlickMoreVisibleAsync(token);
                    break;
            }
        }

        private async UniTask FlickLessVisibleAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _ghostController.SetGhostVisibility(true);
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(
                    _ghostFlickConfig.FlickLessVisibilityVisibleMin, 
                    _ghostFlickConfig.FlickLessVisibilityVisibleMax)), cancellationToken: token);

                _ghostController.SetGhostVisibility(false);
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(
                    _ghostFlickConfig.FlickLessVisibilityInvisibleMin,
                    _ghostFlickConfig.FlickLessVisibilityInvisibleMax)), cancellationToken: token);
            }
        }

        private async UniTask FlickMoreVisibleAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _ghostController.SetGhostVisibility(true);
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(
                    _ghostFlickConfig.FlickMoreVisibilityVisibleMin, 
                    _ghostFlickConfig.FlickMoreVisibilityVisibleMax)), cancellationToken: token);

                _ghostController.SetGhostVisibility(false);
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(
                    _ghostFlickConfig.FlickMoreVisibilityInvisibleMin,
                    _ghostFlickConfig.FlickMoreVisibilityInvisibleMax)), cancellationToken: token);
            }
        }
        
        protected virtual void HuntInterference()
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

        public bool IsSeePlayer(Vector3 direction, int layerToExclude)
        {
            if (Physics.Raycast(_ghostController.transform.position, direction,
                    out var hit, Mathf.Infinity, layerToExclude))
            {
                return hit.collider.TryGetComponent<IPlayer>(out var _);
            }
            return false;
        }

        public bool IsSeeElectronic()
        {
            return false;
        }

        public void MoveToPoint(Vector3 point, bool toPlayer )
        {
            isMoveToPlayer = toPlayer;
            
            _ghostController.SetSpeed(huntingSpeed);
            _ghostController.SetGhostAnimationSpeed(huntingSpeed / _activityData.MaxGhostSpeed);
            _ghostController.MoveTo(point);
        }

        protected void StopHunt()
        {
            _isHunt = false;
            _ghostController.SetGhostVisibility(false);
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
            
            _appearProcess?.Dispose();
            _huntProcess?.Dispose();
            _flickProcess?.Dispose();
            
            _huntCancellationTokenSource?.Cancel();
            _flickCancellationTokenSource?.Cancel();
        }
    }
}
