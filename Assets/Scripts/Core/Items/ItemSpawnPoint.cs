using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}