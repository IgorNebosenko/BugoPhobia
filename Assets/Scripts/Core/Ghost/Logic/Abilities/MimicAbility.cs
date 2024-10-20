using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class MimicAbility : IGhostAbility
    {
        public bool IsInterrupt { get; set; }

        public MimicAbility()
        {
            Debug.Log("Mimic must have non-standard ability!");
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