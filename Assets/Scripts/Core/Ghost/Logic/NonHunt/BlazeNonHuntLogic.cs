using ElectrumGames.Core.Ghost.Controllers;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class BlazeNonHuntLogic : INonHuntLogic
    {
        private readonly GhostController _ghostController;

        public BlazeNonHuntLogic(GhostController ghostController)
        {
            _ghostController = ghostController;
        }

        public void Setup(GhostVariables variables, GhostConstants constants)
        {
        }

        public void FixedSimulate()
        {
        }

        public void MoveToPoint(Transform point)
        {
        }

        public bool TryThrowItem()
        {
            return false;
        }

        public bool TryUseDoor()
        {
            return false;
        }

        public bool TrySwitchInteract()
        {
            return false;
        }

        public bool TryOtherInteraction()
        {
            return false;
        }
    }
}