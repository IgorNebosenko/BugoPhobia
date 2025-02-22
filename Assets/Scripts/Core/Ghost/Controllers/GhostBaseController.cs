using System.Collections.Generic;
using ElectrumGames.Audio.Pool;
using ElectrumGames.Audio.Steps;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Logic;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Missions;
using ElectrumGames.Extensions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public abstract class GhostBaseController : MonoBehaviour, IHaveNetId, IHaveVisibility
    {
        [SerializeField] protected Transform modelRoot;
        
        [field: Space]
        [field: SerializeField] public GhostStepsHandler GhostStepsHandler { get; protected set; }

        private GhostModelController _ghostModelController;
        protected DiContainer _container;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        
        public INonHuntLogic NonHuntLogic { get; protected set; }
        public IGhostEventLogic GhostEventLogic { get; protected set; }
        public IHuntLogic HuntLogic { get; protected set; }
        public IGhostAbility GhostAbility { get; protected set; }
        public FuseBoxCounterLogic FuseBoxCounter { get; protected set; }
        
        public EvidenceController EvidenceController { get; private set; }
        public GhostBehaviourController GhostBehaviourController { get; protected set; }
        public GhostLookController GhostLookController { get; private set; }
        public GhostHuntController GhostHuntController { get; private set; }
        public GhostInteractionController GhostInteractionController { get; private set; }
        public GhostEnvironmentHandler GhostEnvironmentHandler { get; private set; }
        public JournalManager JournalManager { get; private set; }
        
        public bool IsVisible { get; private set; }

        private IReadOnlyList<IGhostController> _ghostControllers;

        protected IFuseBoxInteractable fuseBox;
        
        public void Init(GhostEnvironmentHandler environmentHandler, GhostModelsList modelsList, 
            EvidenceController evidenceController, JournalManager journalManager, IFuseBoxInteractable fuseBox, 
            DiContainer container)
        {
            GhostEnvironmentHandler = environmentHandler;
            GhostBehaviourController = new GhostBehaviourController(GhostEnvironmentHandler);
            GhostLookController = new GhostLookController();
            GhostHuntController = new GhostHuntController();
            GhostInteractionController = new GhostInteractionController();

            this.fuseBox = fuseBox;

            _container = container;

            _ghostControllers = new IGhostController[]
            {
                GhostBehaviourController,
                GhostLookController,
                GhostHuntController,
                GhostInteractionController
            };
            
            EvidenceController = evidenceController;
            JournalManager = journalManager;

            var ghostModel = environmentHandler.GhostVariables.isMale
                ? modelsList.MaleModels.PickRandom()
                : modelsList.FemaleModels.PickRandom();

            _ghostModelController = Instantiate(ghostModel.GhostModelController, Vector3.zero, Quaternion.identity, transform);
            
            GhostStepsHandler.Init(
                _container.Resolve<SoundsConfig>(),
                _container.Resolve<AudioSourcesPool>(),
                _container.Resolve<SurfaceSoundsList>());
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
            IsVisible = isOn;
            
            _ghostModelController.SetVisibility(isOn);
        }
    }
}