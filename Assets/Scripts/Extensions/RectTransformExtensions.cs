using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class RectTransformExtensions
    {
        public static RectTransform GetRectTransform(this Component obj)
        {
            return obj.transform as RectTransform;
        }
    }
}