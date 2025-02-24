using System;
using UnityEngine;

namespace ElectrumGames.Audio.Ghosts
{
    [Serializable]
    public class GhostHuntingSoundGroup
    {
        [field: SerializeField] public AudioClip[] Data { get; private set; }
    }

    [Serializable]
    public class GhostHuntingSoundElement
    {
        [field: SerializeField] public GhostHuntSounds GroupName { get; private set; }
        [field: SerializeField] public GhostHuntingSoundGroup[] Groups { get; private set; }
        [field: Space]
        [field: SerializeField] public bool CanUseMale { get; private set; }
        [field: SerializeField] public bool CanUseFemale { get; private set; }
        [field: Space]
        [field: SerializeField] public float MinCooldownBetweenSounds { get; private set; }
        [field: SerializeField] public float MaxCooldownBetweenSounds { get; private set; }
    }

    [CreateAssetMenu(fileName = "GhostHuntingSounds", menuName = "Audio Config/Ghost Hunting Sounds")]
    public class GhostHuntingSounds : ScriptableObject
    {
        [field: SerializeField] public GhostHuntingSoundElement[] Elements { get; private set; }
    }
}