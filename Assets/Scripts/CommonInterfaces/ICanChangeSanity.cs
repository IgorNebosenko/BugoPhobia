namespace ElectrumGames.CommonInterfaces
{
    public interface ICanChangeSanity
    {
        float CurrentSanity { get; }
        void ChangeSanity(float value, int ownerId = -1);
    }
}