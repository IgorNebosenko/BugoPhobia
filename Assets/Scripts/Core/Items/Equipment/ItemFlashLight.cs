using DG.Tweening;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemFlashLight : ItemInstanceBase, IGhostHuntingInteractable
    {
        [SerializeField] private Light lightSource;
        [SerializeField] private float flickerSpeed = 0.1f;

        private bool _isOn;

        private float _startIntensity;
        private Tween _flickerProcess;

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

        public void OnGhostHuntInteractionEnter()
        {
            _flickerProcess = lightSource.DOIntensity(0f, flickerSpeed)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Flash);
        }

        public void OnGhostHuntInteractionExit()
        {
            _flickerProcess.Kill();
            
            lightSource.intensity = _startIntensity;
        }
    }
}