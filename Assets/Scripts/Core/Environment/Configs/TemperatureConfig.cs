using UnityEngine;

namespace ElectrumGames.Core.Environment.Configs
{
    [CreateAssetMenu(fileName = "TemperatureConfig", menuName = "Environment/Temperature Config")]
    public class TemperatureConfig : ScriptableObject
    {
        [field: SerializeField] public float MinEvidenceTemperature { get; private set; } = -12f;
        [field: SerializeField] public float MinNonEvidenceTemperature { get; private set; } = 0f;
        [field: Space] 
        [field: SerializeField] public float MinRoomChangeTemperatureTime { get; private set; } = 25f;
        [field: SerializeField] public float MaxRoomChangeTemperatureTime { get; private set; } = 55f;
        [field: Space]
        [field: SerializeField] public float MinTimeChangeOneDegree { get; private set; } = 3f;
        [field: SerializeField] public float MaxTimeChangeOneDegree { get; private set; } = 12f;
    }
}