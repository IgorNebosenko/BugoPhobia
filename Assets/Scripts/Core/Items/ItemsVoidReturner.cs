using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items
{
    public class ItemsVoidReturner : MonoBehaviour
    {
        private ItemMarkers _itemMarkers;
        
        [Inject]
        private void Construct(ItemMarkers itemMarkers)
        {
            _itemMarkers = itemMarkers;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ItemInstanceBase>(out var instance))
            {
                var spawnData = _itemMarkers.ItemSpawnPoints[instance.SpawnerId];
                
                instance.transform.position = spawnData.Position;
                instance.transform.rotation = spawnData.Rotation;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
        }
    }
}