using System.Collections.Generic;
using System.Linq;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Ghost.Logic;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Rooms;
using ElectrumGames.GlobalEnums;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Ghost
{
    public class GhostFactory : MonoBehaviour, IHaveNetId
    {
        [SerializeField] private GhostController ghostControllerTemplate;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }

        private NetIdFactory _netIdFactory;
        private GhostEnvironmentHandler _ghostEnvironmentHandler;
        private GhostModelsList _modelsList;
        private GhostDifficultyList _ghostDifficultyList;
        private ActivityConfig _activityConfig;
        private GhostEmfZonePool _ghostEmfZonePool;
        private EmfData _emfData;
        private EvidenceController _evidenceController;

        [Inject]
        private void Construct(NetIdFactory netIdFactory, GhostEnvironmentHandler ghostEnvironmentHandler, GhostModelsList modelsList,
            GhostDifficultyList ghostDifficultyList, ActivityConfig activityConfig, GhostEmfZonePool ghostEmfZonePool, EmfData emfData,
            EvidenceController evidenceController)
        {
            _netIdFactory = netIdFactory;
            _ghostEnvironmentHandler = ghostEnvironmentHandler;
            _modelsList = modelsList;
            _ghostDifficultyList = ghostDifficultyList;
            _activityConfig = activityConfig;
            _ghostEmfZonePool = ghostEmfZonePool;
            _emfData = emfData;
            _evidenceController = evidenceController;

            _netIdFactory.Initialize(this);
        }

        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public GhostBaseController CreateGhost(IReadOnlyList<Room> rooms)
        {
            var ghost = Instantiate(ghostControllerTemplate, transform);
            _netIdFactory.Initialize(ghost);
            
            ghost.Init(_ghostEnvironmentHandler, _modelsList, _evidenceController);
            ghost.SetGhostAnimationSpeed(0f);
            Debug.LogWarning("Ghost is visible!!!");
            //ghost.SetGhostVisibility(false);
            ghost.SetRoomsData(rooms);
            ghost.TeleportToSpawnPoint();
            
            ghost.ActivateAgent();
            
            SetLogic(ghost, _ghostEnvironmentHandler);
            
            return ghost;
        }

        private void SetLogic(GhostController controller, GhostEnvironmentHandler environmentHandler)
        {
            INonHuntLogic nonHuntLogic = null;
            IGhostEventLogic ghostEventLogic = null;
            IHuntLogic huntLogic = null;
            IGhostAbility ghostAbility = null;
            
            switch (controller.GhostEnvironmentHandler.GhostVariables.ghostType)
            {
                case GhostType.Blaze:
                    Debug.Log("Difficulty must read from data!!");
                    nonHuntLogic = new BlazeNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[0], 
                        _activityConfig.GhostActivities.First(x => x.GhostType == GhostType.Blaze),
                        _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new BlazeBaseGhostEventLogic(controller);
                    huntLogic = new BlazeHuntLogic(controller);
                    ghostAbility = new PlaceholderGhostAbility();
                    break;
            }
            
            nonHuntLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            ghostEventLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            huntLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            ghostAbility?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            
            
            controller.SetLogic(nonHuntLogic, ghostEventLogic, huntLogic, ghostAbility);
        }
    }
}