using UnityEngine;

namespace ElectrumGames.Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "User/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [field: SerializeField] public int StartLevelOfXp { get; private set; }
        [SerializeField] private float multiplierPerLevel;

        public int GetRequiredXpAtLevel(int level)
        {
            if (level == 0)
                return 0;
            
            float previousXp = StartLevelOfXp;

            for (var i = 1; i < level; i++)
            {
                previousXp *= multiplierPerLevel;
            }

            return Mathf.RoundToInt(previousXp);
        }

        public int RequiredTotalXp(int currentLevel)
        {
            if (currentLevel == 0)
                return 0;

            float xp = StartLevelOfXp;
            var previousXp = xp;

            for (var i = 1; i < currentLevel; i++)
            {
                previousXp *= multiplierPerLevel;
                xp += previousXp;
            }
            
            return Mathf.RoundToInt(xp);
        }
        
        public int GetLevelByTotalXp(int totalXp)
        {
            float xp = StartLevelOfXp;
            var previousXp = xp;
            var level = 1;

            while (totalXp >= xp)
            {
                previousXp *= multiplierPerLevel;
                xp += previousXp;
                ++level;
            }

            return level;
        }
    }
}