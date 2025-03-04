using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [CreateAssetMenu(fileName = "TorchConfig", menuName = "Ghosts configs/Torch Config")]
    public class TorchConfig : ScriptableObject
    {
        [field: SerializeField] public float TorchCooldown { get; private set; } = 25f;
    }
}