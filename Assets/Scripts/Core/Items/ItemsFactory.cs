using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items 
{
    public class ItemsFactory : MonoBehaviour, IHaveNetId
    {
        private NetIdFactory _netIdFactory;
        private ItemsConfig _itemsConfig;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }
        
        [Inject]
        private void Construct(NetIdFactory netIdFactory, ItemsConfig itemsConfig)
        {
            _netIdFactory = netIdFactory;
            _itemsConfig = itemsConfig;

            _netIdFactory.Initialize(this);
        }
        
        
    }
}
