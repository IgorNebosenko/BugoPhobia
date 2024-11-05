using ElectrumGames.Core.Ghost.Configs;
using Zenject;

namespace ElectrumGames.Core.Player
{
    public interface IPlayerSpawnable
    {
        void Spawn(DiContainer container, GhostDifficultyData difficultyData, bool isPlayablePlayer, bool isHost);
        void Despawn();
    }
}