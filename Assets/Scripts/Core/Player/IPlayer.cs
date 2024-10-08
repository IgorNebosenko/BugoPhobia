using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Core.Player.Sanity;
using ElectrumGames.Extensions.CommonInterfaces;
using UnityEngine;

namespace ElectrumGames.Core.Player
{
    public interface IPlayer : IHaveNetId, IHavePosition
    {
        IInventory Inventory { get; }
        ISanity Sanity { get; }
        void Spawn(PlayerConfig config, ConfigService configService, bool isHost, InputActions inputActions, 
            ItemsConfig itemsConfig, GhostDifficultyData difficultyData, Camera injectedCamera);
        void Despawn();

        int GetCurrentStayRoom();
    }
}
