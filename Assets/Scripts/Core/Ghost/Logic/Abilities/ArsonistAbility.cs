using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class ArsonistAbility : IGhostAbility
    {
        public bool IsInterrupt { get; set; }

        public ArsonistAbility()
        {
            Debug.LogError("Arsonist must have non-standard ability");
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