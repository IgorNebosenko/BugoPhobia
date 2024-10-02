using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions.Pools
{
    public class GhostEmfZonePool : MonoBehaviour
    {
        [SerializeField] private GhostEmfZone cylinderZoneTemplate;
        [SerializeField] private GhostEmfZone sphereZoneTemplate;
        [SerializeField] private int initialSize = 8;
        
        private List<GhostEmfZone> _cylinderZones = new();
        private List<GhostEmfZone> _sphereZones = new();

        private void Awake()
        {
            for (var i = 0; i < initialSize; i++)
            {
                AddCylinderItem();
                AddSphereItem();
            }
        }

        private void AddCylinderItem()
        {
            var item = Instantiate(cylinderZoneTemplate, transform);
            item.gameObject.SetActive(false);
            _cylinderZones.Add(item);
        }

        private void AddSphereItem()
        {
            var item = Instantiate(sphereZoneTemplate, transform);
            item.gameObject.SetActive(false);
            _sphereZones.Add(item);
        }


        public GhostEmfZone SpawnCylinderZone(Transform parent, float heightOffset, Vector3 scale, int emfLevel)
        {
            Debug.Log("SpawnCylinderZone!");
            if (_cylinderZones.Count == 0)
            {
                AddCylinderItem();
            }

            var zone = _cylinderZones.First();

            _cylinderZones.Remove(zone);
            zone.gameObject.SetActive(true);
            zone.transform.parent = parent;

            zone.transform.localPosition = Vector3.up * heightOffset;
            zone.transform.localScale = scale;
            zone.SetLevel(emfLevel);

            return zone;
        }
        
        public GhostEmfZone SpawnSphereZone(Transform parent, float heightOffset, float scale, int emfLevel)
        {
            Debug.Log("SpawnSphereZone!");
            if (_sphereZones.Count == 0)
            {
                AddSphereItem();
            }

            var zone = _sphereZones.First();

            _sphereZones.Remove(zone);
            zone.gameObject.SetActive(true);
            zone.transform.parent = parent;

            zone.transform.localPosition = Vector3.up * heightOffset;
            zone.transform.localScale = Vector3.one * scale;
            zone.SetLevel(emfLevel);

            return zone;
        }

        public void DespawnCylinderZone(GhostEmfZone zone)
        {
            Debug.Log("DespawnCylinderZone!");
            zone.transform.position = Vector3.zero;
            zone.transform.localScale = Vector3.one;
            zone.transform.parent = transform;
            zone.SetLevel(0);
            zone.gameObject.SetActive(false);
            
            _cylinderZones.Add(zone);
        }
        
        public void DespawnSphereZone(GhostEmfZone zone)
        {
            Debug.Log("DespawnSphereZone!");
            zone.transform.position = Vector3.zero;
            zone.transform.localScale = Vector3.one;
            zone.transform.parent = transform;
            zone.SetLevel(0);
            zone.gameObject.SetActive(false);
            
            _sphereZones.Add(zone);
        }
    }
}