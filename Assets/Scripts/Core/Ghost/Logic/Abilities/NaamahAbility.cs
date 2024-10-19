using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class NaamahAbility : IGhostAbility
    {
        public NaamahAbility()
        {
            Debug.LogError("Naamah must to stole items!");
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