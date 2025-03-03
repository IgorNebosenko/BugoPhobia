using UnityEngine;

namespace ElectrumGames.Core.Environment.Configs
{
    [CreateAssetMenu(fileName = "TemperatureConfig", menuName = "Environment/Temperature Config")]
    public class TemperatureConfig : ScriptableObject
    {
        [field: SerializeField] public float MinEvidenceTemperature { get; private set; } = -9.9f;
        [field: SerializeField] public float MinNonEvidenceTemperature { get; private set; } = 0.1f;
        [field: Space]
        [field: SerializeField] public float MinTimeChangeOneDegree { get; private set; } = 3f;
        [field: SerializeField] public float MaxTimeChangeOneDegree { get; private set; } = 12f;
    }
}