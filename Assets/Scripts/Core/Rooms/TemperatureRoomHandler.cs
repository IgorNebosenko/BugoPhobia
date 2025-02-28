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
            if (!_isInProcess && !Mathf.Approximately(CurrentTemperature, _targetTemperature))
            {
                StartCoroutine(MoveToTemperature());
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
        
        private IEnumerator MoveToTemperature()
        {
            _isInProcess = true;

            var timePerDegree = Random.Range(_temperatureConfig.MinTimeChangeOneDegree,
                _temperatureConfig.MaxTimeChangeOneDegree);
            
            float startTemperature = CurrentTemperature;
            float targetDegree = Mathf.RoundToInt(startTemperature);

            while (Mathf.Approximately(targetDegree, Mathf.RoundToInt(_targetTemperature)))
            {
                if (targetDegree < Mathf.RoundToInt(_targetTemperature))
                    targetDegree += 1f;
                else
                    targetDegree -= 1f;

                float startTime = Time.time;
                
                while (Time.time - startTime <= timePerDegree)
                {
                    CurrentTemperature = Mathf.Lerp(startTemperature, targetDegree, (Time.time - startTime) / timePerDegree);
                    
                    yield return null;
                }

                startTemperature = targetDegree;
            }

            CurrentTemperature = _targetTemperature;

            _isInProcess = false;
        }
    }
}
