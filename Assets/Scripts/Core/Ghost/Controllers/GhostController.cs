using System.Collections.Generic;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.Core.Rooms;
using UnityEngine;
using UnityEngine.AI;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public class GhostController : GhostBaseController
    {
        [SerializeField] private NavMeshAgent navmeshAgent;
        
        public INonHuntLogic NonHuntLogic { get; private set; }
        public IGhostEventLogic GhostEventLogic { get; private set; }
        public IHuntLogic HuntLogic { get; private set; }
        public IGhostAbility GhostAbility { get; private set; }

        private IReadOnlyList<Room> _rooms;

        private void FixedUpdate()
        {
            NonHuntLogic?.FixedSimulate();
            GhostEventLogic?.FixedSimulate();
            HuntLogic?.FixedSimulate();
            GhostAbility?.FixedSimulate();
        }

        public void SetRoomsData(IReadOnlyList<Room> rooms)
        {
            _rooms = rooms;
        }
        
        public void TeleportToSpawnPoint()
        {
            var currentRoom = _rooms[GhostEnvironmentHandler.GhostRoomId];

            transform.position = currentRoom.GhostRoomHandler.SpawnPointAtRoom.position;
            transform.rotation = currentRoom.GhostRoomHandler.SpawnPointAtRoom.rotation;
        }

        public void ActivateAgent()
        {
            navmeshAgent.enabled = true;
        }

        public void MoveTo(Vector3 position)
        {
            navmeshAgent.SetDestination(position);
        }

        public void SetSpeed(float speed)
        {
            navmeshAgent.speed = speed;
        }

        public void SetLogic(INonHuntLogic nonHuntLogic, IGhostEventLogic ghostEventLogic, IHuntLogic huntLogic, 
            IGhostAbility ghostAbility)
        {
            NonHuntLogic = nonHuntLogic;
            GhostEventLogic = ghostEventLogic;
            HuntLogic = huntLogic;
            GhostAbility = ghostAbility;
        }
    }
}