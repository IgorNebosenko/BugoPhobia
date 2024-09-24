using ElectrumGames.Core.Environment.Configs;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Environment
{
    public class EnvironmentHandler : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        [field: Space]
        [field: SerializeField] public float OutDoorRadiation { get; private set; }
        
        public WeatherConfigData WeatherData { get; protected set; }
        
        public float OutDoorTemperature { get; private set; }
        
        protected WeatherConfig weatherConfig;

        [Inject]
        private void Construct(WeatherConfig weatherConfig)
        {
            this.weatherConfig = weatherConfig;
            
            SetParams();
            ApplyParams();
        }

        protected virtual void SetParams()
        {
            WeatherData = weatherConfig.Config[Random.Range(0, weatherConfig.Config.Count)];
            OutDoorTemperature = WeatherData.OutdoorTemperature;
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
    }
}