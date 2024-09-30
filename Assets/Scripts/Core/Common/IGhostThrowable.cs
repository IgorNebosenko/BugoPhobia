using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public interface IGhostThrowable
    {
        void ThrowItem(Vector3 direction, float force);
    }
}