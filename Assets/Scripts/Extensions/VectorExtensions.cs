using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 PerValueMul(this Vector3 vec, Vector3 vec2)
        {
            vec.x *= vec2.x;
            vec.y *= vec2.y;
            vec.z *= vec2.z;
            return vec;
        }
    }
}