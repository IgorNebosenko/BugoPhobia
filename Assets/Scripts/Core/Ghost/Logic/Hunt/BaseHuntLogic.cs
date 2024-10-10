using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Rooms;
using ElectrumGames.GlobalEnums;
using UniRx;
using UnityEngine;
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

            _huntProcess = Observable.Start(() => Hunt(huntToken)).Subscribe();
            _flickProcess = Observable.Start(() => Flick(flickToken)).Subscribe();
        }

        protected virtual async Task Hunt(CancellationToken token)
        {
            try
            {
                Debug.Log("Hunt normally started");
                await Task.Delay(TimeSpan.FromSeconds(_ghostDifficultyData.HuntDuration), token);
                Debug.Log("Hunt normally ended!");
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Hunt was cancelled or other issue");
            }
            finally
            {
                StopHunt();
            }
        }

        protected virtual void Flick(CancellationToken token)
        {
            _flickCancellationTokenSource?.Cancel();
            _flickCancellationTokenSource = new CancellationTokenSource();
            token = _flickCancellationTokenSource.Token;

            switch (_ghostConstants.ghostVisibility)
            {
                case GhostVisibility.Invisible:
                    _ghostController.SetGhostVisibility(false);
                    break;

                case GhostVisibility.LessVisible:
                    Observable.FromCoroutine(() => FlickLessVisibleAsync(token)).Subscribe();
                    break;

                case GhostVisibility.MoreVisible:
                    Observable.FromCoroutine(() => FlickMoreVisibleAsync(token)).Subscribe();
                    break;
            }
        }

        private IEnumerator FlickLessVisibleAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _ghostController.SetGhostVisibility(true);
                yield return new WaitForSeconds(Random.Range(
                    _ghostFlickConfig.FlickLessVisibilityVisibleMin, 
                    _ghostFlickConfig.FlickLessVisibilityVisibleMax));

                _ghostController.SetGhostVisibility(false);
                yield return new WaitForSeconds(Random.Range(
                    _ghostFlickConfig.FlickLessVisibilityInvisibleMin,
                    _ghostFlickConfig.FlickLessVisibilityInvisibleMax));
            }
        }

        private IEnumerator FlickMoreVisibleAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _ghostController.SetGhostVisibility(true);
                yield return new WaitForSeconds(Random.Range(
                    _ghostFlickConfig.FlickMoreVisibilityVisibleMin, 
                    _ghostFlickConfig.FlickMoreVisibilityVisibleMax));

                _ghostController.SetGhostVisibility(false);
                yield return new WaitForSeconds(Random.Range(
                    _ghostFlickConfig.FlickMoreVisibilityInvisibleMin,
                    _ghostFlickConfig.FlickMoreVisibilityInvisibleMax));
            }
        }

        public bool IsSeePlayer()
        {
            return false;
        }

        public void MoveToPoint(Vector3 point)
        {
        }

        protected void StopHunt()
        {
            Debug.Log("StopHunt");
            
            _isHunt = false;
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
            
            _appearProcess?.Dispose();
            _huntProcess?.Dispose();
            _flickProcess?.Dispose();
            
            _huntCancellationTokenSource?.Cancel();
            _flickCancellationTokenSource?.Cancel();
            
            _ghostController.SetGhostVisibility(false);
        }
    }
}
