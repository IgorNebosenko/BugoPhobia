using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class MimicAbility : IGhostAbility
    {
        private readonly GhostFactory _ghostFactory;
        private readonly GhostController _ghostController;
        private readonly GhostEmfZonePool _emfZonePool;
        private readonly EmfData _emfData;
        
        private float _cooldown;
        private GhostConstants _ghostConstants;
        
        public bool IsInterrupt { get; set; }

        public MimicAbility(GhostFactory ghostFactory, GhostController ghostController, GhostEmfZonePool emfZonePool,
            EmfData emfData)
        {
            _ghostFactory = ghostFactory;
            _ghostController = ghostController;
            _emfZonePool = emfZonePool;
            _emfData = emfData;
        }
        
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostConstants = constants;
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;
            
            _cooldown += Time.fixedDeltaTime;

            if (_cooldown >= _ghostConstants.abilityCooldown)
            {
                _cooldown = 0f;
                
                if (Random.Range(0f, 1f) < _ghostConstants.abilityChance)
                {
                    TryUseAbility();
                }
            }
        }

        public bool TryUseAbility()
        {
            var ghostType = (GhostType) Random.Range(0, (int) GhostType.Lich);
            Debug.Log($"Non hunt logic will be as {ghostType}");
            var nonHuntLogic = _ghostFactory.GetNonHuntLogicByGhostType(_ghostController, ghostType);
            
            ghostType = (GhostType) Random.Range(0, (int) GhostType.Lich);
            Debug.Log($"Ghost event logic will be as {ghostType}");
            var ghostEventLogic = _ghostFactory.GetGhostEventLogicByType(_ghostController, ghostType);
            
            ghostType = (GhostType) Random.Range(0, (int) GhostType.Lich);
            Debug.Log($"Hunt logic will be as {ghostType}");
            var huntLogic = _ghostFactory.GetHuntLogicByType(_ghostController, ghostType);
            
            _ghostController.SetLogic(nonHuntLogic, ghostEventLogic, huntLogic,
                this, _emfZonePool, _emfData);
            
            return true;
        }
    }
}