using UnityEngine;

namespace ElectrumGames.Audio.Steps
{
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "Audio Config/Sounds Config")]
    public class SoundsConfig : ScriptableObject
    {
        [field: SerializeField] public float FrequencyStepsPlayer { get; private set; } = 0.75f;
        [field: SerializeField] public float FrequencyStepsGhost { get; private set; } = 0.75f;
        [field: Space] 
        [field: SerializeField] public float DefaultPlayerStepVolume { get; private set; } = 0.35f;
        [field: SerializeField] public float DefaultGhostStepVolume { get; private set; } = 1f;
        [field: Space]
        [field: SerializeField] public int DefaultPoolAudioSourcesSize { get; private set; } = 16;
    }
}