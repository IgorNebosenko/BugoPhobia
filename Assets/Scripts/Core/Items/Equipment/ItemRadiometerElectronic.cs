using System;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Environment;
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
        [Space]
        [SerializeField] private Color colorLow;
        [SerializeField] private Color colorMedium;
        [SerializeField] private Color colorHigh;
        [Space]
        [SerializeField] private float radiationValueMedium = 0.06f;
        [SerializeField] private float radiationValueEvidence = 0.12f;
        [Space]
        [SerializeField] private float updateInterval = 10f;
        [SerializeField] private float radiusOverlapDetection = 0.5f;
        [SerializeField] private float differenceRadiation = 0.005f;
        
        private bool _isOn;
        private bool _ghostInteractionOn;

        private float _lastValue;
        
        private Collider[] _colliders = new Collider[CollidersCount];
        private const int CollidersCount = 16;

        private EnvironmentHandler _environmentHandler;
        
        public bool IsElectricityOn => _isOn;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(updateInterval)).Subscribe(UpdateAction).AddTo(this);
        }

        protected override void OnAfterInit()
        {
            Debug.Log("OnAfterInit");
            _environmentHandler = container.Resolve<EnvironmentHandler>();
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
                    var minRad = room.RadiationRoomHandler.Radiation - differenceRadiation;
                    var maxRad = room.RadiationRoomHandler.Radiation + differenceRadiation;

                    if (minRad < 0)
                        minRad = 0.001f;
                    if (maxRad > 9.999f)
                        maxRad = 9.999f;

                    _lastValue = Random.Range(minRad, maxRad);
                    
                    DisplayRadiation(_lastValue);
                    
                    isRoomFounded = true;
                    break;
                }
            }

            if (!isRoomFounded)
            {
                var minRad = _environmentHandler.OutDoorRadiation - differenceRadiation;
                var maxRad = _environmentHandler.OutDoorRadiation + differenceRadiation;
                    
                if (minRad < 0)
                    minRad = 0.001f;
                if (maxRad > 9.999f)
                    maxRad = 9.999f;
                
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

            if (value < radiationValueMedium)
            {
                backgroundImage.color = colorLow;
                onLight.color = colorLow;
            }
            else if (value < radiationValueEvidence)
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