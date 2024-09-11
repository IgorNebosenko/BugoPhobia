using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemUVStick : ItemInstanceBase
    {
        [SerializeField] private Light sourceLight;
        [SerializeField] private float startLightForce;
        [SerializeField] private float endLightForce;
        [Space]
        [SerializeField] private float normalGlowTime;
        [SerializeField] private float glowDecreaseTime;
        [Space]
        [SerializeField] private float lightOnDuration;

        private bool _isOn;
        private float _lifeTime;
        private Coroutine _lifeTimeProcess;
        private Tween _decayProcess;
        
        public override void OnMainInteraction()
        {
            if (_isOn)
                return;

            _isOn = true;

            sourceLight.gameObject.SetActive(true);
            sourceLight.intensity = 0f;
            sourceLight.DOIntensity(startLightForce, lightOnDuration).OnComplete(StartLightLifetime);
        }

        public override void OnAlternativeInteraction()
        {
            if (_lifeTime > 0 || !_isOn)
                return;

            sourceLight.DOIntensity(startLightForce, lightOnDuration);
            transform.DOShakePosition(lightOnDuration).OnComplete(StartLightLifetime);
        }

        private void StartLightLifetime()
        {
            _lifeTime = normalGlowTime;
            _decayProcess.Kill();
            
            if (_lifeTimeProcess != null)
                StopCoroutine(_lifeTimeProcess);
            _lifeTimeProcess = StartCoroutine(LifetimeCycle());

        }

        private IEnumerator LifetimeCycle()
        {
            while (_lifeTime > 0)
            {
                _lifeTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            _decayProcess = sourceLight.DOIntensity(endLightForce, glowDecreaseTime);
        }
    }
}