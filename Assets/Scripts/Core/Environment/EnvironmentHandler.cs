using System;
using ElectrumGames.Audio;
using ElectrumGames.Core.Environment.Configs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Environment
{
    public class EnvironmentHandler : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        [field: Space]
        [field: SerializeField] public float OutDoorRadiation { get; private set; }
        
        public WeatherConfigData WeatherData { get; protected set; }
        
        public float OutDoorTemperature { get; protected set; }
        
        protected WeatherConfig weatherConfig;
        protected NoiseGenerator noiseGenerator;

        [Inject]
        private void Construct(WeatherConfig weatherConfig, NoiseGenerator noiseGenerator)
        {
            this.weatherConfig = weatherConfig;
            this.noiseGenerator = noiseGenerator;
            
            SetParams();
            ApplyParams();
        }

        protected virtual void SetParams()
        {
            WeatherData = weatherConfig.Config[Random.Range(0, weatherConfig.Config.Count)];
            OutDoorTemperature = WeatherData.OutdoorTemperature;

            SetEnvironmentOutdoor();
        }

        private void ApplyParams()
        {
            RenderSettings.skybox = WeatherData.SkyBoxMaterial;

            directionalLight.intensity = WeatherData.DirectionalLightIntensity;
            directionalLight.color = WeatherData.DirectionalLightColor;
            
            RenderSettings.fog = true;
            RenderSettings.fogDensity = WeatherData.FogDistance;
            RenderSettings.fogColor = WeatherData.FogColor;
            
            RenderSettings.ambientIntensity = WeatherData.AmbientIntensity;
        }

        public void SetEnvironmentOutdoor()
        {
            noiseGenerator.SetPassFrequency(WeatherData.NoiseOutdoorMinFrequency, WeatherData.NoiseOutdoorMaxFrequency);
            noiseGenerator.SetPlayState(true);
        }

        public void SetEnvironmentIndoor()
        {
            noiseGenerator.SetPassFrequency(WeatherData.NoiseIndoorMinFrequency, WeatherData.NoiseIndoorMaxFrequency);
            noiseGenerator.SetPlayState(true);
        }
    }
}