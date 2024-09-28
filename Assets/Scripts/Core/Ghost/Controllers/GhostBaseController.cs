using System.Collections.Generic;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Extensions;
using ElectrumGames.GlobalEnums;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public abstract class GhostBaseController : MonoBehaviour, IHaveNetId
    {
        [SerializeField] protected Transform modelRoot;

        private GhostModelController _ghostModelController;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        
        public GhostBehaviourController GhostBehaviourController { get; protected set; }
        public GhostLookController GhostLookController { get; private set; }
        public GhostHuntController GhostHuntController { get; private set; }
        public GhostInteractionController GhostInteractionController { get; private set; }
        public GhostEnvironmentHandler GhostEnvironmentHandler { get; private set; }

        private IReadOnlyList<IGhostController> _ghostControllers;
        
        public void Init(GhostEnvironmentHandler environmentHandler, GhostModelsList modelsList)
        {
            GhostEnvironmentHandler = environmentHandler;
            GhostBehaviourController = new GhostBehaviourController(GhostEnvironmentHandler);
            GhostLookController = new GhostLookController();
            GhostHuntController = new GhostHuntController();
            GhostInteractionController = new GhostInteractionController();

            _ghostControllers = new IGhostController[]
            {
                GhostBehaviourController,
                GhostLookController,
                GhostHuntController,
                GhostInteractionController
            };

            var ghostModel = environmentHandler.GhostVariables.isMale
                ? modelsList.MaleModels.PickRandom()
                : modelsList.FemaleModels.PickRandom();

            _ghostModelController = Instantiate(ghostModel.GhostModelController, Vector3.zero, Quaternion.identity, transform);
        }

        private void FixedUpdate()
        {
            if (_ghostControllers == null)
                return;
            
            for (var i = 0; i < _ghostControllers.Count; i++)
                _ghostControllers[i].FixedSimulate();
        }
        
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public void SetGhostAnimationSpeed(float speed)
        {
            _ghostModelController.SetSpeed(speed);
        }

        public void SetGhostVisibility(bool isOn)
        {
            _ghostModelController.SetVisibility(isOn);
        }
    }
}