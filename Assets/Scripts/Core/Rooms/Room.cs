using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] public int RoomId { get; private set; }
        [Space]
        [SerializeField] private BoxCollider boxCollider;
        [Space]
        [SerializeField] private LightRoomHandler lightRoomHandler;
        [SerializeField] private TemperatureRoomHandler temperatureRoom;
        [SerializeField] private GhostRoomHandler ghostRoomHandler;
        [SerializeField] private DoorsRoomHandler doorsRoomHandler;
        [Space]
        [SerializeField] private Room[] neighborRooms;
    }
}
