using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions.Pools
{
    public class GhostEmfZonePool : MonoBehaviour
    {
        [SerializeField] private GhostEmfZone zoneTemplate;
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
            var item = Instantiate(zoneTemplate, transform);
            item.gameObject.SetActive(false);
            zones.Add(item);
        }


        public GhostEmfZone Spawn(Vector3 position, Vector3 scale, int emfLevel)
        {
            if (zones.Count == 0)
            {
                AddItem();
            }

            var zone = zones.First();

            zone.transform.position = position;
            zone.transform.localScale = scale;
            zone.SetLevel(emfLevel);

            return zone;
        }

        public void Despawn(GhostEmfZone zone)
        {
            zone.transform.position = Vector3.zero;
            zone.transform.localScale = Vector3.one;
            zone.SetLevel(0);
            
            zones.Add(zone);
        }
    }
}