using DG.Tweening;
using UnityEngine;

namespace ElectrumGames.Core.Player.Interactions.Items
{
    public class FlashLightInteractionHandler : MonoBehaviour
    {
        [SerializeField] private Light smallLight;
        [SerializeField] private Light mediumLight;
        [SerializeField] private Light bigLight;
        [Space]
        [SerializeField] private float flickerSpeedMin = 0.1f;
        [SerializeField] private float flickerSpeedMax = 0.3f;
        
        private bool _isFlicking;
        
        private float _smallLightStartIntensity;
        private float _mediumLightStartIntensity;
        private float _bigLightStartIntensity;
        
        private void Start()
        {
            _smallLightStartIntensity = smallLight.intensity;
            _mediumLightStartIntensity = mediumLight.intensity;
            _bigLightStartIntensity = bigLight.intensity;
        }
        
        public bool IsElectricityOn { get; private set; }

        public void EnableSmallLight()
        {
            smallLight.enabled = true;
            mediumLight.enabled = false;
            bigLight.enabled = false;

            IsElectricityOn = true;
        }
        
        public void EnableMediumLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = true;
            bigLight.enabled = false;
            
            IsElectricityOn = true;
        }
        
        public void EnableBigLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = false;
            bigLight.enabled = true;
            
            IsElectricityOn = true;
        }
        
        public void DisableLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = false;
            bigLight.enabled = false;
            
            IsElectricityOn = false;
        }

        public void OnGhostInterferenceStay()
        {
            if (_isFlicking)
                return;

            _isFlicking = true;

            var durationDecrease = Random.Range(flickerSpeedMin, flickerSpeedMax);
            var durationIncrease = Random.Range(flickerSpeedMin, flickerSpeedMax);

            var sequence = DOTween.Sequence();
            
            sequence.Join(smallLight.DOIntensity(0f, durationDecrease).SetEase(Ease.Flash).
                OnComplete(() => smallLight.DOIntensity(_smallLightStartIntensity, durationIncrease)));
            
            sequence.Join(mediumLight.DOIntensity(0f, durationDecrease).SetEase(Ease.Flash).
                OnComplete(() => mediumLight.DOIntensity(_mediumLightStartIntensity, durationIncrease)));
            
            sequence.Join(bigLight.DOIntensity(0f, durationDecrease).SetEase(Ease.Flash).
                OnComplete(() => bigLight.DOIntensity(_bigLightStartIntensity, durationIncrease)));

            sequence.OnComplete(() => _isFlicking = false);
        }
    }
}