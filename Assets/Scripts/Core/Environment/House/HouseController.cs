using System.Collections.Generic;
using ElectrumGames.Core.Ghost;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Rooms;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Environment.House
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField] private FuseBoxEnvironmentObject fuseBox;
        [Space]
        [SerializeField] private int minGhostId = 0;
        [SerializeField] private int maxGhostId = 19;
        [Space]
        [SerializeField] private int minRoomId = 0;
        [SerializeField] private int maxRoomId = 0;
        [Space]
        [SerializeField] private Room[] rooms;

        private GhostFactory _ghostFactory;
        
        public bool IsKeyPicked { get; private set; }
        
        public bool IsEnterDoorOpened { get; set; }
        public IReadOnlyList<Room> Rooms => rooms;

        [Inject]
        private void Construct(GhostEnvironmentHandler ghostEnvironmentHandler, GhostFactory ghostFactory)
        {
            ghostEnvironmentHandler.InitGhost(minGhostId, maxGhostId, minRoomId, maxRoomId);
            _ghostFactory = ghostFactory;
        }

        public void OnPickUpKey()
        {
            IsKeyPicked = true;

            for (var i = 0; i < rooms.Length; i++)
                rooms[i].DoorsRoomHandler.UnlockDoors();
        }

        private void OnEnable()
        {
            _ghostFactory.GhostCreated += OnGhostCreated;
            HouseKeyEnvironmentObject.PickUpKey += OnPickUpKey;
        }

        private void OnDisable()
        {
            _ghostFactory.GhostCreated -= OnGhostCreated;
            HouseKeyEnvironmentObject.PickUpKey -= OnPickUpKey;
        }
        
        private void OnGhostCreated(GhostBaseController ghost)
        {
            ghost.HuntLogic.HuntStarted += CloseAllClosableDoors;
            ghost.HuntLogic.HuntStarted += LockAllSwitches;
            
            ghost.HuntLogic.HuntEnded += OpenAllClosableDoors;
            ghost.HuntLogic.HuntEnded += UnlockAllSwitches;
            
            ghost.HuntLogic.HuntEnded += SwitchOffAllLight;
        }

        private void CloseAllClosableDoors()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i].DoorsRoomHandler.BlockDoors();
            }
        }

        private void OpenAllClosableDoors()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i].DoorsRoomHandler.UnBlockDoors();
            }
        }

        private void SwitchOffAllLight()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i].LightRoomHandler.SwitchOffLight();
            }
        }

        private void LockAllSwitches()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i].LightRoomHandler.LockSwitch();
            }
        }

        private void UnlockAllSwitches()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i].LightRoomHandler.UnlockSwitch();
            }
        }
    }
}