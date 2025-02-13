using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.GlobalEnums;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items 
{
    public class ItemsFactory : MonoBehaviour, IHaveNetId
    {
        private NetIdFactory _netIdFactory;
        private ItemsConfig _itemsConfig;
        private PlayerConfig _playerConfig;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }
        
        [Inject]
        private void Construct(NetIdFactory netIdFactory, ItemsConfig itemsConfig, PlayerConfig playerConfig)
        {
            _netIdFactory = netIdFactory;
            _itemsConfig = itemsConfig;
            _playerConfig = playerConfig;

            _netIdFactory.Initialize(this);
        }

        public ItemInstanceBase Spawn(ItemSpawnPoint spawnPoint, int id)
        {
            var template = _itemsConfig.GetItemByType(spawnPoint.ItemType).ItemInstance;
            
            var item = Instantiate(template, 
                spawnPoint.Position, spawnPoint.Rotation, transform);
            _netIdFactory.Initialize(item);
            item.Init(_playerConfig, this, id);
            return item;
        }
    }
}
