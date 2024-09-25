namespace ElectrumGames.Core.Environment
{
    public class DemoEnvironmentHandler : EnvironmentHandler
    {
        
        protected override void SetParams()
        {
            WeatherData = weatherConfig.Config[0];
        }
    }
}