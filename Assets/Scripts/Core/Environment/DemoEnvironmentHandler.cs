using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class DemoEnvironmentHandler : EnvironmentHandler
    {
        
        protected override void SetParams()
        {
            Debug.Log("Weather sets explicit!");
            WeatherData = weatherConfig.Config[6];
            OutDoorTemperature = WeatherData.OutdoorTemperature;

            SetEnvironmentOutdoor();
        }
    }
}