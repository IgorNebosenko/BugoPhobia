using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class PoltergeistAbility : IGhostAbility
    {
        public bool IsInterrupt { get; set; }

        public PoltergeistAbility()
        {
            Debug.LogError("Poltergeist must to have ability!");
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