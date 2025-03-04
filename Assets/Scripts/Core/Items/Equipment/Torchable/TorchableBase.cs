namespace ElectrumGames.Core.Items.Equipment.Torchable
{
    public abstract class TorchableBase : ItemInstanceBase
    {
        public float ChanceTorch { get; protected set; }
        
        public abstract void Torch();
    }
}