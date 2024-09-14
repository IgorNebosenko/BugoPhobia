using DG.Tweening;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemFlashLight : ItemInstanceBase, IGhostHuntingInteractable
    {
        [SerializeField] private Light lightSource;
        [SerializeField] private float flickerSpeedMin = 0.1f;
        [SerializeField] private float flickerSpeedMax = 0.3f;

        private bool _isOn;
        private bool _isFlicking;

        private float _startIntensity;

        public bool IsElectricityOn => _isOn;

        private void Start()
        {
            _startIntensity = lightSource.intensity;
        }

        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            lightSource.enabled = _isOn;
        }

        public override void OnAlternativeInteraction()
        {
        }

        public void OnGhostInteractionStay()
        {
            if (_isFlicking)
                return;

            _isFlicking = true;
            
            lightSource.DOIntensity(0f, Random.Range(flickerSpeedMin, flickerSpeedMax))
                .SetEase(Ease.Flash).
                OnComplete(() => lightSource.DOIntensity(_startIntensity, 
                    Random.Range(flickerSpeedMin, flickerSpeedMax)).
                    OnComplete(() =>_isFlicking = false));
        }
    }
}