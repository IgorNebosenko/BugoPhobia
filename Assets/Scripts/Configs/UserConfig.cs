using UnityEngine;

namespace ElectrumGames.Configs
{
    [CreateAssetMenu(fileName = "UserConfig", menuName = "User/UserConfig")]
    public class UserConfig : ScriptableObject
    {
        [field: SerializeField] public float MinXSensitivity { get; private set; } = 1f;
        [field: SerializeField] public float DefaultXSensitivity { get; private set; } = 6f;
        [field: SerializeField] public float MaxXSensitivity { get; private set; } = 30f;
        
        [field: Space]
        [field: SerializeField] public float MinYSensitivity { get; private set; } = 1f;
        [field: SerializeField] public float DefaultYSensitivity { get; private set; } = 6f;
        [field: SerializeField] public float MaxYSensitivity { get; private set; } = 30f;
        
        [field: Space]
        [field: SerializeField] public int MinInvertXValue { get; private set; } = 0;
        [field: SerializeField] public int DefaultInvertXValue { get; private set; } = 0;
        [field: SerializeField] public int MaxInvertXValue { get; private set; } = 1;
        
        [field: Space]
        [field: SerializeField] public int MinInvertYValue { get; private set; } = 0;
        [field: SerializeField] public int DefaultInvertYValue { get; private set; } = 0;
        [field: SerializeField] public int MaxInvertYValue { get; private set; } = 1;
        
        [field: Space]
        [field: SerializeField] public int MinHeadBobValue { get; private set; } = 0;
        [field: SerializeField] public int DefaultHeadBobValue { get; private set; } = 1;
        [field: SerializeField] public int MaxHeadBobValue { get; private set; } = 1;
        
        [field: Space]
        [field: SerializeField] public float MinFOV { get; private set; } = 60f;
        [field: SerializeField] public float DefaultFOV { get; private set; } = 90f;
        [field: SerializeField] public float MaxFOV { get; private set; } = 90f;
    }
}