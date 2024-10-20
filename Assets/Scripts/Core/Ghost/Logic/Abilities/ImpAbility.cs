using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class ImpAbility : IGhostAbility
    {
        public bool IsInterrupt { get; set; }

        public ImpAbility()
        {
            Debug.LogError("Imp must to have non-standard ability logic!");
        }
        
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
        }

        public void FixedSimulate()
        {
        }

        public bool TryUseAbility()
        {
            return false;
        }
    }
}