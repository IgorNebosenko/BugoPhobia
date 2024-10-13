using ElectrumGames.Core.Environment;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class DoorsRoomHandler : MonoBehaviour
    {
        [SerializeField] private DoorEnvironmentObject[] doors;

        public void BlockDoors()
        {
            for (var i = 0; i < doors.Length; i++)
                doors[i].BlockDoor();
        }

        public void UnBlockDoors()
        {
            for (var i = 0; i < doors.Length; i++)
                doors[i].UnBlockDoor();
        }

        public void UnlockDoors()
        {
            for (var i = 0; i < doors.Length; i++)
                doors[i].UnlockDoor();
        }

        public void CloseDoors()
        {
            for (var i = 0; i < doors.Length; i++)
                doors[i].CloseDoor();
        }
    }
}