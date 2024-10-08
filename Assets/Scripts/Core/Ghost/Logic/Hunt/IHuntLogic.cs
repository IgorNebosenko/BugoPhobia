using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public interface IHuntLogic : IGhostLogic
    {
        bool IsSeePlayer();
        void MoveToPoint(Vector3 point);
    }
}