using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public interface INonHuntLogic : IGhostLogic
    {
        void MoveToPoint(Transform point);

        bool TryThrowItem();
        bool TryUseDoor();
        bool TrySwitchInteract();
        bool TryOtherInteraction();
    }
}