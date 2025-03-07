using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public interface IStartHuntInteractable
    {
        int CountUsesRemain { get; }
        float RadiusUse { get; }
        Transform Transform { get; }
        bool OnHuntInteraction();
    }
}