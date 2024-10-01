using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public interface INonHuntLogic : IGhostLogic
    {
        void MoveToPoint(Vector3 point);
    }
}