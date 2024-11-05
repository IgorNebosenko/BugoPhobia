using ElectrumGames.CommonInterfaces;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Core.Player.Sanity;
using ElectrumGames.Extensions.CommonInterfaces;
using UnityEngine;

namespace ElectrumGames.Core.Player
{
    public interface IPlayer : IHaveNetId, IHavePosition, IHavePlayerStatus, IHaveInventory, IPlayerSpawnable
    {
        Transform PlayerHead { get; }
        ISanity Sanity { get; }
        FlashLightInteractionHandler FlashLightInteractionHandler { get; }

        void OnGhostInterferenceStay();
        int GetCurrentStayRoom();
        void Death();
    }
}
