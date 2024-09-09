using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public abstract class ItemInstanceBase : MonoBehaviour, IHaveNetId
    {
        [field: SerializeField] public Rigidbody physicObject { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }

        private PlayerConfig _playerConfig;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public void Init(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public abstract void OnMainInteraction();
        public abstract void OnAlternativeInteraction();

        public void OnDropItem(Transform playerTransform)
        {
            OwnerId = -1;

            var dropForce = Random.Range(_playerConfig.MinDropForce, _playerConfig.MaxDropForce);
            
            physicObject.AddForce(playerTransform.forward * dropForce);
        }
    }
}