using System.Collections.Generic;
using ElectrumGames.Core.Rooms;
using UnityEngine;

namespace ElectrumGames.Core.Environment.House
{
    public class TutorialHouseController : MonoBehaviour
    {
        [SerializeField] private FuseBoxEnvironmentObject fuseBox;
        [Space]
        [SerializeField] private Room[] rooms;
        
        public bool IsKeyPicked { get; private set; }
        
        public bool IsEnterDoorOpened { get; set; }
        public IReadOnlyList<Room> Rooms => rooms;
        
        public void OnPickUpKey()
        {
            IsKeyPicked = true;

            for (var i = 0; i < rooms.Length; i++)
                rooms[i].DoorsRoomHandler.UnlockDoors();
        }
        
        private void OnEnable()
        {
            HouseKeyEnvironmentObject.PickUpKey += OnPickUpKey;
        }

        private void OnDisable()
        {
            HouseKeyEnvironmentObject.PickUpKey -= OnPickUpKey;
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