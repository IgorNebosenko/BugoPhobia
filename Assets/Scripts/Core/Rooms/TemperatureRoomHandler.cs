using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class TemperatureRoomHandler : MonoBehaviour
    {
        [SerializeField] private float maxTemperature;
        [SerializeField] private float temperatureCoefficient;
        
        public float CurrentTemperature { get; private set; }
    }
}