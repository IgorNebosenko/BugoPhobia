using ElectrumGames.Core.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        
        private bool _isOn;
        public bool IsElectricityOn => _isOn;
        
        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            onLight.enabled = _isOn;
        }

        public override void OnAlternativeInteraction()
        {
        }
        
        public void OnGhostInteractionStay()
        {
        }

        public void OnGhostInteractionExit()
        {
        }
    }
}