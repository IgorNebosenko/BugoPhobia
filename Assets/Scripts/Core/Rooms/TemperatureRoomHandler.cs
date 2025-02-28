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

        private bool _isInProcess;
        private bool _isInterrupted;
        
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

        private void Update()
        {
            if (!Mathf.Approximately(CurrentTemperature, _targetTemperature))
            {
                var difference = CurrentTemperature - _targetTemperature;

                if (difference != 0)
                {
                    var timePerDegree = Random.Range(_temperatureConfig.MinTimeChangeOneDegree,
                        _temperatureConfig.MaxTimeChangeOneDegree);
                    
                    CurrentTemperature += (difference < 0) ? 
                        Time.deltaTime / timePerDegree : -Time.deltaTime / timePerDegree;
                }
            }
        }

        private void OnDestroy()
        {
            _fuseBox.StateChanged -= ChangeFuseBoxState;
        }

        public void SetTemperatureRoomState(TemperatureRoomState state)
        {
            _roomState = state;

            switch (_roomState)
            {
                case TemperatureRoomState.NoGhost:
                    _targetTemperature = _fuseBox.State
                        ? maxTemperature
                        : _environmentHandler.OutDoorTemperature * temperatureCoefficient;
                    break;
                case TemperatureRoomState.GhostWithoutFreezing:
                    _targetTemperature = _temperatureConfig.MinNonEvidenceTemperature;
                    break;
                case TemperatureRoomState.GhostWithFreezing:
                    _targetTemperature = _temperatureConfig.MinEvidenceTemperature;
                    break;
            }
        }
        
        private void ChangeFuseBoxState(bool state)
        {
            if (_roomState == TemperatureRoomState.NoGhost)
                _targetTemperature =
                    state ? maxTemperature : _environmentHandler.OutDoorTemperature * temperatureCoefficient;
        }
    }
}
