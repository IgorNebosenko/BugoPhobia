using System;
using System.Diagnostics;
using System.Threading;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
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
        
        private const int LayerToExclude = ~(1 << 2);

        public GhostVariables GhostVariables { get; private set; }
        public GhostConstants GhostConstants { get; private set; }
        private int _roomId;

        private float _huntCooldownTime;
        private bool _isSafeTime;
        private bool _isHunt;
        
        protected float huntingSpeed;
        protected bool isMoveToPlayer;

        private CancellationTokenSource _huntCancellationTokenSource;
        private CancellationTokenSource _flickCancellationTokenSource;
        private IDisposable _appearProcess;
        private IDisposable _huntProcess;
        private IDisposable _flickProcess;
        
        public bool IsInterrupt { get; set; }

        public virtual float ChanceThrowItem => 0.5f;
        public virtual float ChanceTouchDoor => 0.5f;

        public event Action HuntStarted;
        public event Action HuntEnded;

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
            GhostVariables = variables;
            GhostConstants = constants;
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
            
            if (!_isHunt)
                _huntCooldownTime += Time.fixedDeltaTime;

            if (_huntCooldownTime >= _activityData.HuntCooldown)
            {
                _huntCooldownTime = 0f;

                if (CanHuntBySanity() && CanHuntByChanceHunt())
                {
                    if (_ghostController.GhostHuntAura.PlayersInAura.Count != 0)
                    {
                        for (var i = 0; i < _ghostController.GhostHuntAura.PlayersInAura.Count; i++)
                        {
                            if (_ghostController.GhostHuntAura.PlayersInAura[i].Inventory.Items[
                                    _ghostController.GhostHuntAura.PlayersInAura[i].InventoryIndexHandler
                                        .CurrentIndex] is
                                IStartHuntInteractable startHuntInteractable)
                            {

                                if (startHuntInteractable.OnHuntInteraction())
                                {
                                    Debug.Log(
                                        $"Hunt interrupted by crucifix in hands of player! Uses remain: {startHuntInteractable.CountUsesRemain}");
                                    StopHunt();
                                    return;
                                }
                            }
                        }
                    }

                    if (_ghostController.GhostHuntAura.StartHuntInteractableList.Count > 0)
                    {
                        for (var i = 0; i < _ghostController.GhostHuntAura.StartHuntInteractableList.Count; i++)
                        {
                            if (_ghostController.GhostHuntAura.StartHuntInteractableList[i].OnHuntInteraction())
                            {
                                Debug.Log(
                                    $"Hunt interrupted by crucifix in ground! Uses remain: " +
                                    $"{_ghostController.GhostHuntAura.StartHuntInteractableList[i].CountUsesRemain}");
                                StopHunt();
                                return;
                            }
                        }
                    }
                    
                    _isHunt = true;
                    HuntStarted?.Invoke();
                    
                    _ghostController.SetEnabledLogic(GhostLogicSelector.Hunt);
                    
                    _ghostController.SetGhostVisibility(true);
                    _ghostController.IsStopped(true);

                    _isSafeTime = true;
                    
                    _appearProcess = Observable.Timer(TimeSpan.FromSeconds(_ghostDifficultyData.SafeHuntTime))
                        .Subscribe(StartHuntProcess);
                }
            }

            if (_isHunt)
            {
                HuntInterference();
                
                if (!_isSafeTime && _ghostController.ContactAura.PlayersInAura is {Count: > 0})
                {
                    for (var i = 0; i < _ghostController.ContactAura.PlayersInAura.Count; i++)
                        _ghostController.ContactAura.PlayersInAura[i].Death();
                }
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
            _isSafeTime = false;
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
                const float distanceTolerance = 0.1f;

                huntingSpeed = GhostConstants.defaultHuntingSpeed;

                while (stopWatch.Elapsed.Seconds < _ghostDifficultyData.HuntDuration)
                {
                    if (!isMoving)
                    {
                        isMoving = true;

                        MoveToPoint(GetHuntMovePosition(), false);
                    }
                    else if (_ghostController.NavmeshRemainingDistance < distanceTolerance)
                    {
                        isMoving = false;
                    }

                    ThrowItemsOnHunt();
                    TouchDoorsOnHunt();
                    
                    SpeedChange();

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

        protected virtual Vector3 GetHuntMovePosition()
        {
            return _huntPoints.Positions.PickRandom().position;
        }

        protected virtual void CheckPlayerOnVisual()
        {
            if (_ghostController.GhostHuntAura.PlayersInAura is {Count: > 0})
            {
                
                for (var i = 0; i < _ghostController.GhostHuntAura.PlayersInAura.Count; i++)
                {
                    var directionToPlayer = _ghostController.GhostHuntAura.PlayersInAura[i].PlayerHead.position -
                                            _ghostController.transform.position;

                    if (IsSeePlayer(directionToPlayer)) //Todo check is chase player by isMoveToPlayer
                        MoveToPoint(_ghostController.GhostHuntAura.PlayersInAura[i].PlayerHead.position, true);
                }
            }
        }

        protected virtual void CheckPlayerOnElectronic()
        {
            if (_ghostController.GhostHuntAura.PlayersInAura is {Count: > 0})
            {
                for (var i = 0; i < _ghostController.GhostHuntAura.PlayersInAura.Count; i++)
                {
                    if (IsSeeElectronic(_ghostController.GhostHuntAura.PlayersInAura[i]))
                        MoveToPoint(_ghostController.GhostHuntAura.PlayersInAura[i].PlayerHead.position, true);
                }
            }
        }

        protected virtual void ThrowItemsOnHunt()
        {
            if (_ghostController.InteractionAura.ThrownInTrigger is {Count: > 0})
            {
                for (var i = 0; i < _ghostController.InteractionAura.ThrownInTrigger.Count; i++)
                {
                    if (Random.Range(0f, 1f) > ChanceThrowItem)
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
                    if (Random.Range(0f, 1f) > ChanceTouchDoor)
                        continue;
                    
                    _ghostController.InteractionAura.DoorsInTrigger[i].TouchDoor(
                        Random.Range(GhostConstants.minDoorAngle, GhostConstants.maxDoorAngle),
                        Random.Range(GhostConstants.minDoorTouchTime, GhostConstants.maxDoorTouchTime));
                }
            }
        }

        protected virtual void SpeedChange()
        {
            if (!GhostConstants.hasSpeedUp)
                return;

            if (_ghostController.GhostHuntAura.PlayersInAura is {Count: > 0})
            {
                //ToDo speed up if any player seen
                var isSeePlayer = false;
                
                for (var i = 0; i < _ghostController.GhostHuntAura.PlayersInAura.Count; i++)
                {
                    var directionToPlayer = _ghostController.GhostHuntAura.PlayersInAura[i].PlayerHead.position -
                                            _ghostController.transform.position;
                    if (IsSeePlayer(directionToPlayer)) //ToDo need to check is it chased player
                    {
                        isSeePlayer = true;
                        break;
                    }
                }

                if (isSeePlayer)
                    SpeedUp();
                else 
                    SpeedDown();

            }
            else
            {
                SpeedDown();
            }
        }

        protected virtual void SpeedUp()
        {
            huntingSpeed += _ghostDifficultyData.SpeedUpByIteration;
            
            Debug.Log("Need read max limit of speed from config!");
            if (huntingSpeed >= 3f)
                huntingSpeed = 3f;
            
            _ghostController.SetSpeed(huntingSpeed);
        }

        protected virtual void SpeedDown()
        {
            huntingSpeed -= _ghostDifficultyData.SpeedUpByIteration;

            if (huntingSpeed < GhostConstants.defaultHuntingSpeed)
                huntingSpeed = GhostConstants.defaultHuntingSpeed;
            
            _ghostController.SetSpeed(huntingSpeed);
        }

        protected virtual async UniTask Flick(CancellationToken token)
        {
            switch (GhostConstants.ghostVisibility)
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
                    
                    _ghostController.GhostEventAura.PlayersInAura[i].OnGhostInterferenceStay();
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

        public bool IsSeePlayer(Vector3 direction)
        {
            if (Physics.Raycast(_ghostController.transform.position, direction,
                    out var hit, Mathf.Infinity, LayerToExclude))
            {
                return hit.collider.TryGetComponent<IPlayer>(out var _);
            }
            return false;
        }

        public IPlayer GetPlayerAt(Vector3 direction)
        {
            if (Physics.Raycast(_ghostController.transform.position, direction,
                    out var hit, Mathf.Infinity, LayerToExclude))
            {
                if (hit.collider.TryGetComponent<IPlayer>(out var player))
                    return player;
            }
            return null;
        }

        public bool IsSeeElectronic(IPlayer player)
        {
            var electricalObject = player.Inventory.Items[player.InventoryIndexHandler.CurrentIndex] as 
                IGhostHuntingHasElectricity;

            var result = electricalObject is {IsElectricityOn: true};

            if (!result)
                result = player.FlashLightInteractionHandler.IsElectricityOn;
            
            return result;
        }

        public bool IsHearPlayer()
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

        public void ForceHunt()
        {
            _huntCooldownTime = Mathf.Infinity;
        }

        protected virtual void StopHunt()
        {
            _isHunt = false;
            _ghostController.SetGhostVisibility(false);
            
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
            
            _appearProcess?.Dispose();
            _huntProcess?.Dispose();
            _flickProcess?.Dispose();
            
            _huntCancellationTokenSource?.Cancel();
            _flickCancellationTokenSource?.Cancel();
            
            HuntEnded?.Invoke();
        }
    }
}
