using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public interface IFuseBoxInteractable
    {
        Transform FuseBoxTransform { get; }
        bool TryInteract(bool canSwitchOn);
    }
}