using ElectrumGames.Core.Common;
using TMPro;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemSpiritBoxElectronic : ItemInstanceBase,
        IGhostHuntingHasElectricity, IGhostHuntingInteractableStay, IGhostHuntingInteractableExit
    {
        [SerializeField] private TMP_Text frequencyText;
        [SerializeField] private Light onLight;
        [SerializeField] private float minFrequency = 88f;
        [SerializeField] private float maxFrequency = 110f;
        [Space]
        [SerializeField] private float frequencyStep = 0.1f;
        [SerializeField] private float frequencyStepTime = 0.2f;
        [Space]
        [SerializeField] private string textOff = "OFF";
        [SerializeField] private string textOnFormat = "{0:0.0}";
        [Space] 
        [SerializeField] private float responsePause = 1f;
        [SerializeField] private string textNoResponse = "NO RESPONSE";
        [SerializeField] private string textResponse = "GHOST";
        
        private bool _isOn;

        private float _currentFrequency;
        private bool _isFrequencyIncrease = true;
        private float _currentTime;

        private bool _ghostInteractionOn;

        private float _responsePauseTime;
        
        public bool IsElectricityOn => _isOn;

        private void Awake()
        {
            _currentFrequency = minFrequency;
        }

        private void FixedUpdate()
        {
            if (!_isOn)
            {
                frequencyText.text = textOff;
                return;
            }
            if (_ghostInteractionOn)
                return;

            if (_responsePauseTime > 0)
            {
                _responsePauseTime -= Time.fixedDeltaTime;
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            
            if (_currentTime > 0)
                return;

            _currentTime = frequencyStepTime;

            if (_isFrequencyIncrease)
            {
                _currentFrequency += frequencyStep;
                if (_currentFrequency >= maxFrequency)
                    _isFrequencyIncrease = false;
            }
            else
            {
                _currentFrequency -= frequencyStep;

                if (_currentFrequency <= minFrequency)
                    _isFrequencyIncrease = true;
            }
            DisplayFrequency(_currentFrequency);
        }

        public void SetResponse(bool isSuccessful)
        {
            DisplayResponse(isSuccessful);
            //ToDo say from dynamic response
        }

        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            onLight.enabled = _isOn;
        }

        public override void OnAlternativeInteraction()
        {
        }

        private void DisplayFrequency(float frequency)
        {
            frequencyText.text = string.Format(textOnFormat, frequency);
        }

        private void DisplayResponse(bool isSuccessful)
        {
            _responsePauseTime = responsePause;

            frequencyText.text = isSuccessful ? textResponse : textNoResponse;
        }

        public void OnGhostInteractionStay()
        {
            _ghostInteractionOn = true;
            
            DisplayFrequency(Random.Range(minFrequency, maxFrequency));
            onLight.enabled = Random.Range(0, 3) != 0;
        }

        public void OnGhostInteractionExit()
        {
            _ghostInteractionOn = false;
            onLight.enabled = _isOn;
        }
    }
}