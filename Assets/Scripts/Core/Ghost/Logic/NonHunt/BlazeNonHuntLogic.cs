using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class BlazeNonHuntLogic : INonHuntLogic
    {
        private readonly GhostController _ghostController;
        private readonly GhostDifficultyData _ghostDifficultyData;
        
        private GhostVariables _ghostVariables;
        private GhostConstants _ghostConstants;
        private int _roomId;

        public BlazeNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData)
        {
            _ghostController = ghostController;
            _ghostDifficultyData = ghostDifficultyData;
        }

        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostVariables = variables;
            _ghostConstants = constants;
            _roomId = roomId;
        }

        public void FixedSimulate()
        {
            Debug.Log("BlazeNonHuntLogic FixedSimulate");
        }

        public void MoveToPoint(Transform point)
        {
            _ghostController.MoveTo(point.position);
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