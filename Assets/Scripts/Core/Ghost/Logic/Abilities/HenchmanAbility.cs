using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class HenchmanAbility : IGhostAbility
    {
        public bool IsInterrupt { get; set; }

        public HenchmanAbility()
        {
            Debug.LogError("Henchman must to have non-standard ability");
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