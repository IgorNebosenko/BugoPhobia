using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public class YrkaHuntLogic : BaseHuntLogic
    {
        public const float SpeedDownModifier = 0.6f;
        public const float SpeedUpModifier = 1.4f;

        private readonly GhostController _ghostController;
        
        public YrkaHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, MissionPlayersHandler missionPlayersHandler,
            GhostFlickConfig ghostFlickConfig, HuntPoints huntPoints) : base(ghostController, ghostDifficultyData,
            activityData, missionPlayersHandler, ghostFlickConfig, huntPoints)
        {
            _ghostController = ghostController;
        }

        protected override void SpeedChange()
        {
            if (_ghostController.GhostHuntAura.PlayersInAura is {Count: > 0})
            {
                var isSeePlayer = false;
                var isAnyPlayerLookAtGhost = false;
                
                for (var i = 0; i < _ghostController.GhostHuntAura.PlayersInAura.Count; i++)
                {
                    var directionToPlayer = _ghostController.GhostHuntAura.PlayersInAura[i].PlayerHead.position -
                                            _ghostController.transform.position;

                    var player = GetPlayerAt(directionToPlayer) as PlayerBase;
                    
                    if (!player.UnityNullCheck())
                    {
                        isSeePlayer = true;
                        if (!player.LookAtGhostHandler.CheckIsLookAtGhost().UnityNullCheck())
                            isAnyPlayerLookAtGhost = true;
                    }
                }

                if (isSeePlayer)
                {
                    huntingSpeed = GhostConstants.defaultHuntingSpeed;
                    huntingSpeed *= isAnyPlayerLookAtGhost ? SpeedDownModifier : SpeedUpModifier;
                    Debug.Log($"Speed is {huntingSpeed}");
                }
                else
                    huntingSpeed = GhostConstants.defaultHuntingSpeed;

            }
            else
            {
                huntingSpeed = GhostConstants.defaultHuntingSpeed;
            }
        }
    }
}