using System.Linq;
using ElectrumGames.Audio.Pool;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.GlobalEnums;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items
{
    public abstract class ItemInstanceBase : MonoBehaviour, IHaveNetId
    {
        [field: SerializeField] public Rigidbody PhysicObject { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; protected set; }
        [field: SerializeField] public CollisionThrowSound CollisionThrowSound { get; private set; }
        
        private PlayerConfig _playerConfig;
        private ItemsFactory _itemsFactory;
        protected IInventory inventoryReference;

        private AudioSourcesPool _audioSources;
        private ItemThrowSoundConfig _throwSoundConfig;

        protected DiContainer container;
        
        protected bool isInDropState;

        public Vector3 LocalScale { get; private set; } = Vector3.one;//transform.localScale;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public int SpawnerId { get; private set; }

        private void Update()
        {
        }

        public void SetInventory(IInventory inventory)
        {
            inventoryReference = inventory;
        }
        
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public void Init(DiContainer container, ItemsFactory itemsFactory, int spawnerId)
        {
            this.container = container;
            
            _playerConfig = this.container.Resolve<PlayerConfig>();
            _itemsFactory = itemsFactory;
            
            _audioSources = container.Resolve<AudioSourcesPool>();
            _throwSoundConfig = container.Resolve<ItemThrowSoundConfig>();
            
            SpawnerId = spawnerId;

            LocalScale = transform.localScale;

            OnAfterInit();
        }

        protected virtual void OnAfterInit()
        {
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

            isInDropState = true;
            
            OnAfterDrop();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isInDropState && !collision.collider.isTrigger)
            {
                isInDropState = false;
                PlayCollisionDropSound();
            }
        }

        public virtual void OnAfterDrop()
        {
        }

        public void PlayCollisionDropSound()
        {
            var clip = _throwSoundConfig.ItemThrowSoundItems.
                First(x => x.CollisionThrowSound == CollisionThrowSound).AudioClip;

            _audioSources.Spawn(transform.position, clip, 1f);
        }
    }
}