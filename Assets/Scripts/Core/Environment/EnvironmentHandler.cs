using ElectrumGames.Core.Environment.Configs;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Environment
{
    public class EnvironmentHandler : MonoBehaviour
    {
        public WeatherConfigData WeatherData { get; protected set; }
        
        public float OutDoorTemperature { get; private set; }
        
        protected WeatherConfig weatherConfig;

        [Inject]
        private void Construct(WeatherConfig weatherConfig)
        {
            weatherConfig = weatherConfig;
            SetParams();
            ApplyParams();
        }

        protected virtual void SetParams()
        {
            WeatherData = weatherConfig.Config[Random.Range(0, weatherConfig.Config.Count)];
        }

        private void ApplyParams()
        {
        }
    }
}