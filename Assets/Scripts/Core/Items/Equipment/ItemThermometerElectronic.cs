using TMPro;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemThermometerElectronic : ItemInstanceBase, IGhostHuntingInteractable
    {
        [SerializeField] private TMP_Text thermometerText;
        [SerializeField] private Light onLight;
        
        [SerializeField] private string textOff = "OFF";
        [SerializeField] private string textOn = "SCAN";
        [SerializeField] private string textOnFormat = "{0:0.0} C";
        
        private bool _isOn;
        public bool IsElectricityOn { get; private set; }
        
        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            if (_isOn)
            {
                thermometerText.text = textOn;
                onLight.enabled = true;
            }
            else
            {
                thermometerText.text = textOff;
                onLight.enabled = false;
            }
        }
        
        private void DisplayTemperature(float temperature)
        {}

        public override void OnAlternativeInteraction()
        {
        }
        
        public void OnGhostHuntInteractionEnter()
        {
        }

        public void OnGhostHuntInteractionExit()
        {
        }
    }
}