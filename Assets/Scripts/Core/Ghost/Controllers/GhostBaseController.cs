using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public abstract class GhostBaseController : MonoBehaviour
    {
        public GhostBehaviourController GhostBehaviourController { get; protected set; }
        public GhostLookController GhostLookController { get; private set; }
        public GhostHuntController GhostHuntController { get; private set; }
        public GhostInteractionController GhostInteractionController { get; private set; }

        private readonly IReadOnlyList<IGhostController> _ghostControllers;

        public GhostBaseController(GhostEnvironmentHandler environmentHandler)
        {
            GhostBehaviourController = new GhostBehaviourController(environmentHandler);
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
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _ghostControllers.Count; i++)
                _ghostControllers[i].FixedSimulate();
        }
    }
}