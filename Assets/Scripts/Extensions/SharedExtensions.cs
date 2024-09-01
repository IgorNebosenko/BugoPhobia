using System.Collections.Generic;

namespace ElectrumGames.Extensions
{
    public static class SharedExtensions
    {
        public static void Swap<T>(this List<T> list, T a, T b)
        {
            var aIndex = list.FindIndex(obj => obj.Equals(a));
            var bIndex = list.FindIndex(obj => obj.Equals(b));
            var tmp = list[aIndex];
            list[aIndex] = list[bIndex];
            list[bIndex] = tmp;
        }
    }
}