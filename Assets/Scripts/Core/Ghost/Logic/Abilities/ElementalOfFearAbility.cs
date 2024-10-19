using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class ElementalOfFearAbility : IGhostAbility
    {
        public bool IsInterrupt { get; set; }

        public ElementalOfFearAbility()
        {
            Debug.LogError("Elemental of fear must have ability!");
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