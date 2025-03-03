using System;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Environment.Configs;
using ElectrumGames.Core.Rooms;
using TMPro;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Items
{
    public class ItemThermometerElectronic : ItemInstanceBase,
        IGhostHuntingHasElectricity, IGhostHuntingInteractableStay, IGhostHuntingInteractableExit
    {
        [SerializeField] private TMP_Text thermometerText;
        [SerializeField] private Light onLight;
        [Space]
        [SerializeField] private string textOff = "OFF";
        [SerializeField] private string textOn = "SCAN";
        [SerializeField] private string textOnFormat = "{0:0.0} C";
        [SerializeField] private float differenceTemperature = 3f;
        [Space]
        [SerializeField] private float updateInterval = 0.5f;
        [SerializeField] private float radiusOverlapDetection = 0.5f;
        
        private bool _isOn;
        private bool _ghostInteractionOn;

        private Collider[] _colliders = new Collider[CollidersCount];
        private const int CollidersCount = 16;
        
        public bool IsElectricityOn => _isOn;

        private IDisposable _ghostInteractionProcess;

        private EnvironmentHandler _environmentHandler;
        private TemperatureConfig _temperatureConfig;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(updateInterval)).Subscribe(UpdateAction).AddTo(this);
        }

        protected override void OnAfterInit()
        {
            _environmentHandler = container.Resolve<EnvironmentHandler>();
            _temperatureConfig = container.Resolve<TemperatureConfig>();
        }

        private void UpdateAction(long _)
        {
            if (_ghostInteractionOn || !_isOn)
                return;
            
            var size = Physics.OverlapSphereNonAlloc(transform.position, radiusOverlapDetection, _colliders);

            var isRoomFounded = false;
            
            for (var i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent<Room>(out var room))
                {
                    var minTemp = room.TemperatureRoom.CurrentTemperature - differenceTemperature;
                    var maxTemp = room.TemperatureRoom.CurrentTemperature + differenceTemperature;

                    if (minTemp < 0f && room.TemperatureRoom.CurrentTemperature > 0f)
                        minTemp = 0.1f;

                    if (minTemp < _temperatureConfig.MinEvidenceTemperature)
                        minTemp = _temperatureConfig.MinEvidenceTemperature;
                    
                    DisplayTemperature(Random.Range(minTemp, maxTemp));
                    
                    isRoomFounded = true;
                    break;
                }
            }

            if (!isRoomFounded)
            {
                {
                    var minTemp = _environmentHandler.OutDoorTemperature - differenceTemperature;
                    var maxTemp = _environmentHandler.OutDoorTemperature + differenceTemperature;

                    if (minTemp < 0f && _environmentHandler.OutDoorTemperature > 0f)
                        minTemp = 0.1f;
                    
                    DisplayTemperature(Random.Range(minTemp, maxTemp));
                }
            }
        }

        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            thermometerText.text = _isOn ? textOn : textOff;
            
            onLight.enabled = _isOn;
        }

        private void DisplayTemperature(float temperature)
        {
            thermometerText.text = string.Format(textOnFormat, temperature);
        }

        public override void OnAlternativeInteraction()
        {
        }
        
        public void OnGhostInteractionStay()
        {
            if (!_isOn)
                return;

            onLight.enabled = Random.Range(0, 100) % 3 != 0;
            DisplayTemperature(Random.Range(-10f, 30f));
        }

        public void OnGhostInteractionExit()
        {
            onLight.enabled = _isOn;
            thermometerText.text = _isOn ? textOn : textOff;
        }
    }
}