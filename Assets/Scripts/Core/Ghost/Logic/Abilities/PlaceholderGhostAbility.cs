namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class PlaceholderGhostAbility : IGhostAbility
    {
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