﻿using System;
using DG.Tweening;
using UniRx;
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
        [SerializeField] private float vibrationStrength;

        private bool _isOn;
        private bool _lifeCycleEnd;
        private IDisposable _lifeTimeProcess;
        private Tween _decayProcess;
        
        public override void OnMainInteraction()
        {
            if (_isOn)
            {
                if (_lifeCycleEnd)
                {
                    sourceLight.DOIntensity(startLightForce, lightOnDuration);
                    transform.DOShakePosition(lightOnDuration, vibrationStrength).OnComplete(StartLightLifetime);
                }
                return;
            }

            _isOn = true;

            sourceLight.gameObject.SetActive(true);
            sourceLight.intensity = 0f;
            sourceLight.DOIntensity(startLightForce, lightOnDuration).OnComplete(StartLightLifetime);
        }

        public override void OnAlternativeInteraction()
        {
        }

        private void StartLightLifetime()
        {
            _lifeCycleEnd = false;
            _decayProcess.Kill();
            
            _lifeTimeProcess?.Dispose();
            _lifeTimeProcess = Observable.Timer(TimeSpan.FromSeconds(normalGlowTime)).Subscribe(LifetimeCycle)
                .AddTo(this);

        }

        private void LifetimeCycle(long _)
        {
            _lifeCycleEnd = true;
            _decayProcess = sourceLight.DOIntensity(endLightForce, glowDecreaseTime);
        }
    }
}