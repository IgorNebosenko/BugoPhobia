using ElectrumGames.CommonInterfaces;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemInstance : MonoBehaviour, IHaveNetId
    {
        [field: SerializeField] public Rigidbody physicObject { get; private set; }
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }
    }
}