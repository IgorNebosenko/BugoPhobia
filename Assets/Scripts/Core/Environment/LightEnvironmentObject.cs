using DG.Tweening;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class LightEnvironmentObject : EnvironmentObjectBase
    {
        [SerializeField] private float normalTemperature;
        [SerializeField] private float redLightTemperature = 1500f;
        [Space]
        [SerializeField] private Light[] lightSources;
        
        [field: SerializeField] public LightEnvironmentMode LightMode { get; private set; }
        public bool IsElectricityOn => false;

        private float[] _defaultLightIntensity;
        private bool _isFlick;

        private FuseBoxEnvironmentObject _fuseBox;

        public void SetRedState(bool isRed)
        {
            for (var i = 0; i < lightSources.Length; i++)
            {
                if (isRed)
                    lightSources[i].colorTemperature = redLightTemperature;
                else
                    lightSources[i].colorTemperature = normalTemperature;
            }
        }

        private void Awake()
        {
            _defaultLightIntensity = new float[lightSources.Length];

            for (var i = 0; i < lightSources.Length; i++)
                _defaultLightIntensity[i] = lightSources[i].intensity;
        }

        private void OnDestroy()
        {
            if (_fuseBox != null)
            {
                _fuseBox.StateChanged -= SetStateByFuseBox;
            }
        }

        public void SwitchStateTo(bool state)
        {
            if (LightMode == LightEnvironmentMode.Broken)
                return;

            for (var i = 0; i < lightSources.Length; i++)
                lightSources[i].enabled = state && _fuseBox.State;

            LightMode = state ? LightEnvironmentMode.Enabled : LightEnvironmentMode.Disabled;
        }

        public void BrokeLight()
        {
            LightMode = LightEnvironmentMode.Broken;
            
            for (var i = 0; i < lightSources.Length; i++)
                lightSources[i].enabled = false;
        }

        public void DoFlick(float flickerSpeedMin, float flickerSpeedMax, bool isRed = false)
        {
            if (_isFlick)
                return;

            _isFlick = true;
            
            for (var i = 0; i < lightSources.Length; i++)
            {
                var j = i;
                
                if (isRed)
                    lightSources[i].colorTemperature = redLightTemperature;
                else
                    lightSources[j].colorTemperature = normalTemperature;
                
                lightSources[j].DOIntensity(0, Random.Range(flickerSpeedMin, flickerSpeedMax)).
                    SetEase(Ease.Flash).
                    SetLoops(1).
                    OnComplete(() => lightSources[j].DOIntensity(_defaultLightIntensity[j], Random.Range(flickerSpeedMin, flickerSpeedMax)).
                        OnComplete(() => _isFlick = false));
            }
        }

        public override void OnInteract()
        {
        }

        public void FuseBoxConnect(FuseBoxEnvironmentObject fuseBox)
        {
            _fuseBox = fuseBox;

            _fuseBox.StateChanged += SetStateByFuseBox;
        }

        private void SetStateByFuseBox(bool state)
        {
            for (var i = 0; i < lightSources.Length; i++)
            {
                lightSources[i].enabled = state && LightMode == LightEnvironmentMode.Enabled;
                //Debug.Log($"State - {state} | LightMode - {LightMode}");
            }
        }
    }
}