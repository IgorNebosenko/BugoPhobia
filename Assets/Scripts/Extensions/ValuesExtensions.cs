using System;
using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class ValuesExtensions
    {
        private static readonly float kLimit = Mathf.Pow(10, 3);
        private static readonly float MLimit = Mathf.Pow(10, 6);
        private static readonly float BLimit = Mathf.Pow(10, 9);
        
        public static string GetShortValue(this int val)
        {
            if (val < kLimit)
                return $"{val}";
            if (val < MLimit)
                return $"{Math.Round(val / kLimit, 2)} k";
            if (val < BLimit)
                return $"{Math.Round(val / MLimit, 2)} M";

            return $"{Math.Round(val / BLimit, 2)} B";
        }
    }
}