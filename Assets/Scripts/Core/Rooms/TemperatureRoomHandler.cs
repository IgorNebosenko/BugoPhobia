using ElectrumGames.Core.Environment;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Rooms
{
    public class TemperatureRoomHandler : MonoBehaviour
    {
        [SerializeField] private float maxTemperature;
        [SerializeField] private float temperatureCoefficient;

        private FuseBoxEnvironmentObject _fuseBox;
        private EnvironmentHandler _environmentHandler;
        
        public float CurrentTemperature { get; private set; }

        [Inject]
        private void Construct(FuseBoxEnvironmentObject fuseBox, EnvironmentHandler environmentHandler)
        {
            _fuseBox = fuseBox;
            _environmentHandler = environmentHandler;
        }

        private void Start()
        {
            if (_fuseBox.State)
            {
                CurrentTemperature = maxTemperature;
            }
            else
            {
                CurrentTemperature = _environmentHandler.OutDoorTemperature * temperatureCoefficient;
            }
        }
    }
}