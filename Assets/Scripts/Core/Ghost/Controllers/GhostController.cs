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
        
        protected INonHuntLogic _nonHuntLogic;
        protected IGhostEventLogic _ghostEventLogic;
        protected IHuntLogic _huntLogic;
        protected IGhostAbility _ghostAbility;

        private IReadOnlyList<Room> _rooms;

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
            _nonHuntLogic = nonHuntLogic;
            _ghostEventLogic = ghostEventLogic;
            _huntLogic = huntLogic;
            _ghostAbility = ghostAbility;
        }
    }
}