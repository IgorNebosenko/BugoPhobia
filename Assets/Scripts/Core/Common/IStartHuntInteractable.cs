using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public interface IStartHuntInteractable
    {
        int CountUsesRemain { get; }
        float RadiusUse { get; }
        Vector3 Position { get; }
        bool OnHuntInteraction();
    }
}