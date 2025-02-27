using System;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items.Zones;
using ElectrumGames.Core.Rooms;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Items
{
    public class ItemRadiometerElectronic : ItemInstanceBase,
        IGhostHuntingHasElectricity, IGhostHuntingInteractableStay, IGhostHuntingInteractableExit
    {
        [SerializeField] private TMP_Text radiationText;
        [SerializeField] private Light onLight;
        [SerializeField] private Image backgroundImage;
        [Space]
        [SerializeField] private string textOff = "OFF";
        [SerializeField] private string textOnFormat = "{0:0.000} mR/h";
        [SerializeField] private float updateInterval = 10f;
        [Space]
        [SerializeField] private Color colorLow;
        [SerializeField] private Color colorMedium;
        [SerializeField] private Color colorHigh;
        [Space]
        [SerializeField] private float radiusOverlapDetection = 0.5f;
        
        private bool _isOn;
        private bool _ghostInteractionOn;

        private float _lastValue;
        
        private Collider[] _colliders = new Collider[CollidersCount];
        private const int CollidersCount = 16;

        private EnvironmentHandler _environmentHandler;
        private RadiationConfig _radiationConfig;
        
        public bool IsElectricityOn => _isOn;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(updateInterval)).
                Subscribe(UpdateAction).AddTo(this);
        }

        protected override void OnAfterInit()
        {
            _environmentHandler = container.Resolve<EnvironmentHandler>();
            _radiationConfig = container.Resolve<RadiationConfig>();
        }

        private void UpdateAction(long _)
        {
            if (_ghostInteractionOn || !_isOn)
                return;
            
            var size = Physics.OverlapSphereNonAlloc(transform.position, radiusOverlapDetection, _colliders);

            var isTargetFounded = false;

            for (var i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent<RadiationGhostZone>(out var radiationGhostZone))
                {
                    var minRad = radiationGhostZone.CurrentRadiation - _radiationConfig.DifferenceRadiation;
                    var maxRad = radiationGhostZone.CurrentRadiation + _radiationConfig.DifferenceRadiation;

                    if (minRad <= 0)
                        minRad = _radiationConfig.MinRadiationValue;
                    if (maxRad > _radiationConfig.MaxRadiationValue)
                        maxRad = _radiationConfig.MaxRadiationValue;

                    _lastValue = Random.Range(minRad, maxRad);
                    
                    DisplayRadiation(_lastValue);
                    isTargetFounded = true;
                    break;
                }
            }

            if (!isTargetFounded)
            {
                for (var i = 0; i < size; i++)
                {
                    if (_colliders[i].TryGetComponent<Room>(out var room))
                    {
                        var minRad = room.RadiationRoomHandler.Radiation - _radiationConfig.DifferenceRadiation;
                        var maxRad = room.RadiationRoomHandler.Radiation + _radiationConfig.DifferenceRadiation;

                        if (minRad <= 0)
                            minRad = _radiationConfig.MinRadiationValue;
                        if (maxRad > _radiationConfig.MaxRadiationValue)
                            maxRad = _radiationConfig.MaxRadiationValue;

                        _lastValue = Random.Range(minRad, maxRad);

                        DisplayRadiation(_lastValue);

                        isTargetFounded = true;
                        break;
                    }
                }
            }

            if (!isTargetFounded)
            {
                var minRad = _environmentHandler.OutDoorRadiation - _radiationConfig.DifferenceRadiation;
                var maxRad = _environmentHandler.OutDoorRadiation + _radiationConfig.DifferenceRadiation;
                    
                if (minRad <= 0)
                    minRad = _radiationConfig.MinRadiationValue;
                if (maxRad > _radiationConfig.MaxRadiationValue)
                    maxRad = _radiationConfig.MaxRadiationValue;
                
                _lastValue = Random.Range(minRad, maxRad);
                    
                DisplayRadiation(_lastValue);
            }
        }

        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            if (!_isOn)
            {
                radiationText.text = textOff;
                backgroundImage.color = colorLow;
                _ghostInteractionOn = false;
            }
            else
                DisplayRadiation(0);
            
            onLight.enabled = _isOn;
        }

        private void DisplayRadiation(float value)
        {
            radiationText.text = string.Format(textOnFormat, value);

            if (value < _radiationConfig.RadiationValueMedium)
            {
                backgroundImage.color = colorLow;
                onLight.color = colorLow;
            }
            else if (value < _radiationConfig.RadiationValueEvidence)
            {
                backgroundImage.color = colorMedium;
                onLight.color = colorMedium;
            }
            else
            {
                backgroundImage.color = colorHigh;
                onLight.color = colorHigh;
            }
        }

        public override void OnAlternativeInteraction()
        {
        }
        
        public void OnGhostInteractionStay()
        {
            _ghostInteractionOn = true;

            if (_isOn)
            {
                DisplayRadiation(Random.Range(0.001f, 9.999f));
                onLight.enabled = Random.Range(0, 3) != 0;
            }
        }

        public void OnGhostInteractionExit()
        {
            _ghostInteractionOn = false;
            DisplayRadiation(_lastValue);
        }
    }
}