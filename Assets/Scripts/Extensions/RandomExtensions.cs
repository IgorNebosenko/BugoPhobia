using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class RandomExtensions
    {
        public static T PickRandom<T>(params T[] vals)
        {
            if (vals.Length == 0)
            {
                Debug.LogError("Please enter at least one value");
                return default(T);
            }

            return vals[Random.Range(0, vals.Length)];
        }

        public static float NextFloat(this System.Random random)
        {
            return (float) random.NextDouble();
        }

        public static float NextFloat(this System.Random random, float minValue, float maxValue)
        {
            return minValue + (float) random.NextDouble() * (maxValue - minValue);
        }
    }
}