using ElectrumGames.Configs;

namespace ElectrumGames.Core.Lobby
{
    public class LevelsHandler
    {
        private readonly LevelsConfig _levelsConfig;
        
        public int CurrentLevel { get; private set; }
        public int CurrentLevelXp { get; private set; }
        public int CurrentLevelMaxXp { get; private set; }

        public LevelsHandler(LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
        }
    }
}