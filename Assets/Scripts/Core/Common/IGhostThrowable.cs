using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public interface IGhostThrowable : IGhostInteractable
    {
        void ThrowItem(float force);
    }
}