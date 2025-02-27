using System.Collections;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Environment.Configs;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Rooms
{
    public class TemperatureRoomHandler : MonoBehaviour
    {
        [SerializeField] private float maxTemperature;
        [SerializeField] private float temperatureCoefficient;

        private float _targetTemperature;
        private TemperatureRoomState _roomState;
        
        private FuseBoxEnvironmentObject _fuseBox;
        private EnvironmentHandler _environmentHandler;
        private TemperatureConfig _temperatureConfig;
        
        public float CurrentTemperature { get; private set; }

        [Inject]
        private void Construct(FuseBoxEnvironmentObject fuseBox, EnvironmentHandler environmentHandler, 
            TemperatureConfig temperatureConfig)
        {
            _fuseBox = fuseBox;
            _environmentHandler = environmentHandler;
            _temperatureConfig = temperatureConfig;
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
            
            _targetTemperature = CurrentTemperature;

            _fuseBox.StateChanged += ChangeFuseBoxState;
        }

        private void OnDestroy()
        {
            _fuseBox.StateChanged -= ChangeFuseBoxState;
        }

        private void ChangeFuseBoxState(bool state)
        {
        }

        public void SetTemperatureRoomState(TemperatureRoomState state)
        {
        }
    }
}