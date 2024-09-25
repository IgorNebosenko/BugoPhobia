using ElectrumGames.Core.Environment;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class DoorsRoomHandler : MonoBehaviour
    {
        [SerializeField] private DoorEnvironmentObject[] doors;


        public void OpenDoors()
        {
            for (var i = 0; i < doors.Length; i++)
                doors[i].OpenDoor();
        }
    }
}