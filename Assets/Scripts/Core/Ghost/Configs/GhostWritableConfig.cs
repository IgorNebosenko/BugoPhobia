using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [CreateAssetMenu(fileName = "GhostWritableConfig", menuName = "Ghosts configs/Ghost Writable Config")]
    public class GhostWritableConfig : ScriptableObject
    {
        [SerializeField] private float minWriteCooldown = 15f;
        [SerializeField] private float maxWriteCooldown = 35f;

        public float WriteCooldown => Random.Range(minWriteCooldown, maxWriteCooldown);
    }
}