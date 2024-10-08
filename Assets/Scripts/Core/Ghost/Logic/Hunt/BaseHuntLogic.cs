using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Missions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public abstract class BaseHuntLogic : IHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        private readonly GhostActivityData _activityData;
        private readonly MissionPlayersHandler _missionPlayersHandler;

        private float _huntCooldownTime;
        private bool _isHunt;
        
        public bool IsInterrupt { get; set; }

        public BaseHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData, 
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = ghostDifficultyData;
            _activityData = activityData;
            _missionPlayersHandler = missionPlayersHandler;
        }
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;
            
            _huntCooldownTime += Time.fixedDeltaTime;

            if (_huntCooldownTime >= _activityData.HuntCooldown)
            {
                _huntCooldownTime = 0f;

                if (CanHuntBySanity() && CanHuntByChanceHunt())
                {
                    _isHunt = true;
                    _ghostController.SetEnabledLogic(GhostLogicSelector.Hunt);
                    
                    Debug.LogAssertion("HUNT!");
                    StopHunt();
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

        public bool IsSeePlayer()
        {
            return false;
        }

        public void MoveToPoint(Vector3 point)
        {
        }

        protected void StopHunt()
        {
            _isHunt = false;
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
        }
    }
}