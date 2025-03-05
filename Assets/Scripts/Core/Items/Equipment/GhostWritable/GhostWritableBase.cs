namespace ElectrumGames.Core.Items.GhostWritable
{
    public abstract class GhostWritableBase : ItemInstanceBase
    {
        public abstract float ChanceWrite { get; }
        public abstract void Write();
    }
}