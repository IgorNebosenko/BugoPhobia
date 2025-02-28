using ElectrumGames.Core.Environment.Configs;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class LobbyEnvironmentHandler : EnvironmentHandler
    {
        [SerializeField] private float outDoorTemperature;
        protected override void SetParams()
        {
            Debug.LogWarning("Weather config for lobby must be read from SO");
            WeatherData = WeatherConfigData.CreateForLobby(125f, 220f);
            SetEnvironmentIndoor();

            OutDoorTemperature = outDoorTemperature;
        }

        protected override void ApplyParams()
        {
        }
    }
}