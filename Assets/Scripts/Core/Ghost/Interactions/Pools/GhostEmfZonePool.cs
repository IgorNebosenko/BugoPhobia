using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions.Pools
{
    public class GhostEmfZonePool : MonoBehaviour
    {
        [SerializeField] private GhostEmfZone cylinderZoneTemplate;
        [SerializeField] private int initialSize = 8;
        
        private List<GhostEmfZone> zones = new();

        private void Awake()
        {
            for (var i = 0; i < initialSize; i++)
            {
                AddItem();
            }
        }

        private void AddItem()
        {
            var item = Instantiate(cylinderZoneTemplate, transform);
            item.gameObject.SetActive(false);
            zones.Add(item);
        }


        public GhostEmfZone Spawn(Transform parent, float heightOffset, Vector3 scale, int emfLevel)
        {
            Debug.Log("Spawn!");
            if (zones.Count == 0)
            {
                AddItem();
            }

            var zone = zones.First();

            zones.Remove(zone);
            zone.gameObject.SetActive(true);
            zone.transform.parent = parent;

            zone.transform.localPosition = Vector3.up * heightOffset;
            zone.transform.localScale = scale;
            zone.SetLevel(emfLevel);

            return zone;
        }

        public void Despawn(GhostEmfZone zone)
        {
            Debug.Log("Despawn!");
            zone.transform.position = Vector3.zero;
            zone.transform.localScale = Vector3.one;
            zone.transform.parent = transform;
            zone.SetLevel(0);
            
            zones.Add(zone);
        }
    }
}