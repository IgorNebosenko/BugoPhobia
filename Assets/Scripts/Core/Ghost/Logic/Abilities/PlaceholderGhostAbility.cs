namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class PlaceholderGhostAbility : IGhostAbility
    {
        public void Setup(GhostVariables variables, GhostConstants constants)
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