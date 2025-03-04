using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [CreateAssetMenu(fileName = "TorchConfig", menuName = "Ghosts configs/Torch Config")]
    public class TorchConfig : ScriptableObject
    {
        [SerializeField] private float minCooldown = 15f;
        [SerializeField] private float maxCooldown = 30f;

        public float TorchCooldown => Random.Range(minCooldown, maxCooldown);
    }
}