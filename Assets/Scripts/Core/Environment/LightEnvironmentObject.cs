using DG.Tweening;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class LightEnvironmentObject : MonoBehaviour
    {
        [SerializeField] private float normalTemperature;
        [SerializeField] private Color redLightColor = Color.red;
        [Space]
        [SerializeField] private Light[] lightSources;
        [Space]
        [SerializeField] private float flickerSpeed = 0.1f;
        
        [field: SerializeField] public LightEnvironmentMode LightMode { get; private set; }

        private float[] _defaultLightIntensity;
        private Tween[] _tweens;

        private void Awake()
        {
            _defaultLightIntensity = new float[lightSources.Length];
            _tweens = new Tween[lightSources.Length];

            for (var i = 0; i < lightSources.Length; i++)
                _defaultLightIntensity[i] = lightSources[i].intensity;
        }

        public void SwitchStateTo(bool state)
        {
            if (LightMode == LightEnvironmentMode.Broken)
                return;

            for (var i = 0; i < lightSources.Length; i++)
                lightSources[i].enabled = state;

            LightMode = state ? LightEnvironmentMode.Enabled : LightEnvironmentMode.Disabled;
        }

        public void BrokeLight()
        {
            LightMode = LightEnvironmentMode.Broken;
            
            for (var i = 0; i < lightSources.Length; i++)
                lightSources[i].enabled = false;
        }

        public void StartFlicker(bool isRed = false)
        {
            EndFlicker();

            for (var i = 0; i < lightSources.Length; i++)
            {
                if (isRed)
                    lightSources[i].color = redLightColor;
                else
                    lightSources[i].colorTemperature = normalTemperature;
                _tweens[i] = lightSources[i].DOIntensity(0, flickerSpeed).SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.Flash);
            }
        }

        public void EndFlicker()
        {
            for (var i = 0; i < lightSources.Length; i++)
            {
                lightSources[i].colorTemperature = normalTemperature;
                lightSources[i].intensity = _defaultLightIntensity[i];
                _tweens[i]?.Kill(true);
            }
        }
    }
}