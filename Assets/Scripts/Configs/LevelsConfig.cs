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
            var xp = 100f;
            var previousXp = xp;

            for (var i = 1; i < level; i++)
            {
                previousXp *= multiplierPerLevel;
                xp += previousXp;
            }

            return Mathf.RoundToInt(xp);
        }

        public int GetLevelByTotalXp(int totalXp)
        {
            var xp = 100f;
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