using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items
{
    public class ItemMarkers : MonoBehaviour
    {
        [SerializeField] private ItemSpawnPoint[] itemSpawnPoints;

        private ItemsFactory _itemsFactory;

        public IReadOnlyList<ItemSpawnPoint> ItemSpawnPoints => itemSpawnPoints;
        
        [Inject]
        private void Construct(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        private void Start()
        {
            for (var i = 0; i < itemSpawnPoints.Length; i++)
            {
                itemSpawnPoints[i].SpawnerId = i;
                _itemsFactory.Spawn(itemSpawnPoints[i], i);
            }
        }
    }
}