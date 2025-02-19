using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Player;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class HenchmanAbility : IGhostAbility
    {
        private readonly MissionPlayersHandler _missionPlayersHandler;
        private readonly GhostController _ghostController;
        
        private float _cooldownTime;

        private GhostConstants _ghostConstants;
        
        public bool IsInterrupt { get; set; }

        public HenchmanAbility(MissionPlayersHandler missionPlayersHandler, GhostController ghostController)
        {
            _missionPlayersHandler = missionPlayersHandler;
            _ghostController = ghostController;
        }
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostConstants = constants;
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;
            
            _cooldownTime += Time.fixedDeltaTime;

            if (_cooldownTime >= _ghostConstants.abilityCooldown)
            {
                _cooldownTime = 0f;

                if (Random.Range(0f, 1f) < _ghostConstants.abilityChance)
                {
                    TryUseAbility();
                }
            }
        }

        public bool TryUseAbility()
        {
            var player = _missionPlayersHandler.Players.PickRandom();

            _ghostController.transform.position = player.Position;
            
            return true;
        }
    }
}