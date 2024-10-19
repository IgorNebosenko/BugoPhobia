using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class BabadukAbility : IGhostAbility
    {
        public BabadukAbility()
        {
            Debug.LogError("There's not implemented babaduk ability!");
        }
        
        public bool IsInterrupt { get; set; }
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