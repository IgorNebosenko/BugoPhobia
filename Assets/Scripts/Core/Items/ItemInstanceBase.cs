using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public abstract class ItemInstanceBase : MonoBehaviour, IHaveNetId
    {
        [field: SerializeField] public Rigidbody PhysicObject { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; protected set; }

        private PlayerConfig _playerConfig;
        private ItemsFactory _itemsFactory;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public void Init(PlayerConfig playerConfig, ItemsFactory itemsFactory)
        {
            _playerConfig = playerConfig;
            _itemsFactory = itemsFactory;
        }
        
        public abstract void OnMainInteraction();
        public abstract void OnAlternativeInteraction();

        public void OnDropItem(Transform playerTransform)
        {
            gameObject.SetActive(true);
            Collider.enabled = true;
            PhysicObject.isKinematic = false;
            
            OwnerId = -1;
            
            var dropForce = Random.Range(_playerConfig.MinDropForce, _playerConfig.MaxDropForce);
            
            PhysicObject.AddForce(playerTransform.forward * dropForce + Vector3.up * _playerConfig.AdditionalLiftForce);
            
            transform.parent = _itemsFactory.transform;
            transform.localScale = Vector3.one;
        }
    }
}