using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public abstract class HaveVisibilityBase : MonoBehaviour, IHaveVisibility
    {
        public bool IsVisible { get; protected set; }
    }
}