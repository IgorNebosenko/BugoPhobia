using ElectrumGames.Configs;
using ElectrumGames.Core.Common;

namespace ElectrumGames.Core.Lobby
{
    public class LevelsHandler
    {
        private const string XpKey = "XP";
        
        private readonly LevelsConfig _levelsConfig;

        public int CurrentLevel => _levelsConfig.GetLevelByTotalXp(XP);
        public int CurrentLevelXp => XP - _levelsConfig.RequiredTotalXp(CurrentLevel - 1);
        public int CurrentLevelMaxXp => _levelsConfig.GetRequiredXpAtLevel(CurrentLevel);

        private int XP
        {
            get => PlayerPrefsAes.GetEncryptedInt(XpKey);
            set => PlayerPrefsAes.SetEncryptedInt(XpKey, value);
        }

        public LevelsHandler(LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
        }
    }
}