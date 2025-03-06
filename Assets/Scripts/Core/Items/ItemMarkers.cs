using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items
{
    public class ItemMarkers : MonoBehaviour
    {
        private List<ItemSpawnPoint> _itemSpawnPoints;

        private ItemsFactory _itemsFactory;

        public IReadOnlyList<ItemSpawnPoint> ItemSpawnPoints => _itemSpawnPoints;
        
        [Inject]
        private void Construct(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        private void Start()
        {
            _itemSpawnPoints = new List<ItemSpawnPoint>();
            
            for (var i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<ItemSpawnPoint>(out var item))
                {
                    item.SpawnerId = i;
                    _itemsFactory.Spawn(item, i);
                    
                    _itemSpawnPoints.Add(item);
                }
            }
        }
    }
}