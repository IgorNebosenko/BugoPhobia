using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [CreateAssetMenu(fileName = "RadiationConfig", menuName = "Ghosts configs/Radiation Config")]
    public class RadiationConfig : ScriptableObject
    {
        [field: SerializeField] public float StartRadiation { get; private set; } = 0.015f;
        [field: SerializeField] public float RadiationValueMedium { get; private set; } = 0.06f;
        [field: SerializeField] public float RadiationValueEvidence { get; private set; } = 0.12f;
        [field: SerializeField] public float RadiationValueEvidenceMax { get; private set; } = 0.42f;
        [field: Space]
        [field: SerializeField] public float UpdateInterval { get; private set; } = 10f;
        [field: SerializeField] public float RadiusOverlapDetection { get; private set; } = 0.5f;
        [field: SerializeField] public float DifferenceRadiation { get; private set; } = 0.005f;
        
        [field: SerializeField] public float MinRadiationValue { get; private set; } = 0.001f;
        [field: SerializeField] public float MaxRadiationValue { get; private set; } = 9.999f;
        
        [field: SerializeField] public float MinAccumulationTime { get; private set; } = 45f;
        [field: SerializeField] public float MaxAccumulationTime { get; private set; } = 125f;
    }
}