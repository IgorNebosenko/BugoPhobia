using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] public int RoomId { get; private set; }
        [Space]
        [SerializeField] private BoxCollider boxCollider;
        [field: Space]
        [field: SerializeField] public LightRoomHandler LightRoomHandler { get; private set; }
        [field: SerializeField] public TemperatureRoomHandler TemperatureRoom { get; private set; }
        [field: SerializeField] public GhostRoomHandler GhostRoomHandler { get; private set; }
        [field: SerializeField] public DoorsRoomHandler DoorsRoomHandler { get; private set; }
        [Space]
        [SerializeField] private Room[] neighborRooms;
    }
}
