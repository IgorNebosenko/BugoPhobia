using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class TemperatureRoomHandler : MonoBehaviour
    {
        [field: SerializeField] public float AverageTemperature { get; private set; }
    }
}