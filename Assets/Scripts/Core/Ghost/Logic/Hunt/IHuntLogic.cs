using System;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Hunt
{
    public interface IHuntLogic : IGhostLogic
    {
        event Action HuntStarted;
        event Action HuntEnded;
        bool IsSeePlayer(Vector3 direction);
        bool IsSeeElectronic(IPlayer player);
        bool IsHearPlayer();
        void MoveToPoint(Vector3 point, bool toPlayer);
    }
}