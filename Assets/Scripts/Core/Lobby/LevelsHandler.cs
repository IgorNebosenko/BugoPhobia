using System;
using ElectrumGames.Configs;
using ElectrumGames.Core.Common;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Lobby
{
    public class LevelsHandler
    {
        private const string XpKey = "XP";

        private readonly int _differenceWithPrevious;
        private readonly LevelsConfig _levelsConfig;

        private int _previousValue;

        public event Action<int> ExperienceUpdated; 
        
        public int CurrentLevel => _levelsConfig.GetLevelByTotalXp(XP);
        public int CurrentLevelXp => XP - _levelsConfig.RequiredTotalXp(CurrentLevel - 1);
        public int CurrentLevelMaxXp => _levelsConfig.GetRequiredXpAtLevel(CurrentLevel);

        public int XP
        {
            get => PlayerPrefsAes.GetEncryptedInt(XpKey);
            set 
            {
                if (_previousValue != XP - _differenceWithPrevious)
                {
                    value = _previousValue + _differenceWithPrevious;
                }
                
                ExperienceUpdated?.Invoke(value);
                PlayerPrefsAes.SetEncryptedDecimal(XpKey, value);

                _previousValue = XP - _differenceWithPrevious;
            }
        }

        public LevelsHandler(LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;

            _differenceWithPrevious = Random.Range(-1000, 1000);
            _previousValue = XP - _differenceWithPrevious;
        }
    }
}