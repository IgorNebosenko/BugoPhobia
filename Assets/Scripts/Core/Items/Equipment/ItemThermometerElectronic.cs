using System;
using ElectrumGames.Core.Common;
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
        
        public bool IsElectricityOn => _isOn;

        private IDisposable _ghostInteractionProcess;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(updateInterval)).Subscribe(UpdateAction).AddTo(this);
        }

        private void UpdateAction(long _)
        {
            if (_ghostInteractionOn || !_isOn)
                return;
            
            var colliders = Physics.OverlapSphere(transform.position, radiusOverlapDetection);

            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<Room>(out var room))
                {
                    var minTemp = room.TemperatureRoom.AverageTemperature - differenceTemperature;
                    var maxTemp = room.TemperatureRoom.AverageTemperature + differenceTemperature;

                    if (minTemp < 0 && room.TemperatureRoom.AverageTemperature > 0)
                        minTemp = 0.1f;
                    
                    DisplayTemperature(Random.Range(minTemp, maxTemp));
                    break;
                }
            }

            if (colliders.Length == 0)
                thermometerText.text = textOn;
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