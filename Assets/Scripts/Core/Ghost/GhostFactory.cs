using System.Collections.Generic;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Logic;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
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

        [Inject]
        private void Construct(NetIdFactory netIdFactory, GhostEnvironmentHandler ghostEnvironmentHandler, GhostModelsList modelsList)
        {
            _netIdFactory = netIdFactory;
            _ghostEnvironmentHandler = ghostEnvironmentHandler;
            _modelsList = modelsList;

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
            
            ghost.Init(_ghostEnvironmentHandler, _modelsList);
            ghost.SetGhostAnimationSpeed(0f);
            Debug.LogWarning("Ghost is visible!!!");
            //ghost.SetGhostVisibility(false);
            ghost.SetRoomsData(rooms);
            ghost.TeleportToSpawnPoint();
            
            ghost.ActivateAgent();
            
            Debug.LogWarning("Ghost move is Debug!!!");
            ghost.SetSpeed(1.8f);
            ghost.MoveTo(new Vector3(27, 0, -9));
            
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
                    nonHuntLogic = new BlazeNonHuntLogic(controller);
                    ghostEventLogic = new BlazeGhostEventLogic(controller);
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