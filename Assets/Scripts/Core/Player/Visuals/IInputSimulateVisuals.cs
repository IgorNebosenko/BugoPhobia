using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;

namespace ElectrumGames.Core.PlayerVisuals
{
    public interface IInputSimulateVisuals
    {
        void Init(ConfigService configService, PlayerConfig playerConfig);
        void Simulate(IInput input, float deltaTime);
    }
}