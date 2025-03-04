using UnityEngine;

namespace ElectrumGames.Core.Items.Equipment.Torchable
{
    public abstract class TorchableBase : ItemInstanceBase
    {
        [field: SerializeField, Range(0f, 1f)]
        public float ChanceTorch { get; protected set; }
        
        public abstract void Torch();
    }
}