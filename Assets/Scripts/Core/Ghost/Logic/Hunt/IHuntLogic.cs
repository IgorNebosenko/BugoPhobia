using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public interface IHuntLogic : IGhostLogic
    {
        bool IsSeePlayer(Vector3 direction, int layerToExclude);
        bool IsSeeElectronic();
        bool IsHearPlayer();
        void MoveToPoint(Vector3 point, bool toPlayer);
    }
}