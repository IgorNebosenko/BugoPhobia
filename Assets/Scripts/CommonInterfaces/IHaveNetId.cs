namespace ElectrumGames.CommonInterfaces
{
    public interface IHaveNetId
    {
        public int NetId { get; }
        public int OwnerId { get; }
        
        void SetNetId(int netId, int ownerId = -1);
    }
}
