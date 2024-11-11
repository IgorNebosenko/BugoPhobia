using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Items.Inventory;
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
        protected IInventory inventoryReference;

        public Vector3 LocalScale => transform.localScale;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public int SpawnerId { get; private set; }

        public void SetInventory(IInventory inventory)
        {
            inventoryReference = inventory;
        }
        
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public void Init(PlayerConfig playerConfig, ItemsFactory itemsFactory, int spawnerId)
        {
            _playerConfig = playerConfig;
            _itemsFactory = itemsFactory;
            SpawnerId = spawnerId;
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
            
            transform.localPosition = Vector3.zero;
            PhysicObject.AddForce(playerTransform.forward * dropForce);
            
            transform.parent = _itemsFactory.transform;
            transform.localScale = LocalScale;
            
            OnAfterDrop();
        }

        public virtual void OnAfterDrop()
        {
        }
    }
}