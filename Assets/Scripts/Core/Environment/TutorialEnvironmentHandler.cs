namespace ElectrumGames.Core.Environment
{
    public class TutorialEnvironmentHandler : EnvironmentHandler
    {
        protected override void SetParams()
        {
            WeatherData = weatherConfig.Config[0];
            OutDoorTemperature = WeatherData.OutdoorTemperature;
        
            SetEnvironmentOutdoor();
        }
    }
}