using ElectrumGames.Core.Environment.Configs;

namespace ElectrumGames.Core.Environment
{
    public class LobbyEnvironmentHandler : EnvironmentHandler
    {
        protected override void SetParams()
        {
            WeatherData = WeatherConfigData.CreateForLobby(200f, 300f);
            SetEnvironmentIndoor();
        }

        protected override void ApplyParams()
        {
        }
    }
}