using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Extensions.CommonInterfaces;
using UnityEngine;

namespace ElectrumGames.Core.Player
{
    public interface IPlayer : IHaveNetId, IHavePosition
    {
        void Spawn(PlayerConfig config, ConfigService configService, bool isHost, InputActions inputActions, Camera injectedCamera);
        void Despawn();
    }
}
