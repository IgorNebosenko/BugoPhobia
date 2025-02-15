using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
using UniRx;
using Debug = UnityEngine.Debug;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class BlazeHuntLogic : BaseHuntLogic
    {
        private bool _isMovingStopped;

        private const float StopTime = 0.5f;
        
        private const float MinHuntSpeed = 1.2f;
        private const float LimitOfHuntSpeed = 2.25f;
        
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        private readonly HuntPoints _huntPoints;
        
        public BlazeHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler, GhostFlickConfig flickConfig,
            HuntPoints huntPoints) : 
            base(ghostController, ghostDifficultyData, activityData, missionPlayersHandler, flickConfig, huntPoints)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = ghostDifficultyData;
            _huntPoints = huntPoints;
        }
        
        protected override async UniTask Hunt(CancellationToken token)
        {
            try
            {
                var isMoving = false;
                const float distanceTolerance = 0.1f;

                huntingSpeed = GhostConstants.defaultHuntingSpeed;

                while (huntingSpeed >= MinHuntSpeed || _isMovingStopped)
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

        protected override void SpeedChange()
        {
            if (_ghostController.GhostHuntAura.PlayersInAura is {Count: > 0})
            {
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
                {
                    if (huntingSpeed >= LimitOfHuntSpeed)
                    {
                        huntingSpeed = GhostConstants.defaultHuntingSpeed;
                    }
                    else
                    {
                        _isMovingStopped = true;
                        huntingSpeed = 0f;
                        Observable.Timer(TimeSpan.FromSeconds(StopTime))
                            .Subscribe(_ =>
                            {
                                huntingSpeed = GhostConstants.defaultHuntingSpeed;
                                _isMovingStopped = false;
                            });
                    }
                }
                else
                {
                    SpeedDown();
                }
            }
            else
            {
                SpeedDown();
            }
        }

        protected override void SpeedDown()
        {
            huntingSpeed -= _ghostDifficultyData.SpeedUpByIteration;
            
            _ghostController.SetSpeed(huntingSpeed);
        }
    }
}