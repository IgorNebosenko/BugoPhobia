namespace ElectrumGames.Core.Player.Sanity
{
    public interface ISanity
    {
        float Sanity { get; }
        
        void ChangeSanity(float value, int ownerId);
    }
}
