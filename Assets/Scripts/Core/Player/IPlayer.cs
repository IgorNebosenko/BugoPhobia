using Core.Items.Inventory;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Core.Player.Sanity;
using ElectrumGames.Extensions.CommonInterfaces;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Player
{
    public interface IPlayer : IHaveNetId, IHavePosition
    {
        bool IsPlayablePlayer { get; }
        bool IsAlive { get; }
        Transform PlayerHead { get; }
        IInventory Inventory { get; }
        InventoryIndexHandler InventoryIndexHandler { get; }
        ISanity Sanity { get; }
        FlashLightInteractionHandler FlashLightInteractionHandler { get; }
        void Spawn(DiContainer container, GhostDifficultyData difficultyData, bool isPlayablePlayer, bool isHost);
        void Despawn();

        int GetCurrentStayRoom();
        void Death();
    }
}
