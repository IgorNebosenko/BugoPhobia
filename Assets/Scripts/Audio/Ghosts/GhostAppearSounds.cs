using System;
using UnityEngine;

namespace ElectrumGames.Audio.Ghosts
{
    [Serializable]
    public class GhostAppearSoundData
    {
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public bool CanUseMale { get; private set; }
        [field: SerializeField] public bool CanUseFemale { get; private set; }
    }

    [CreateAssetMenu(fileName = "GhostAppearSounds", menuName = "Audio Config/Ghost Appear Sounds")]
    public class GhostAppearSounds : ScriptableObject
    {
        [field: SerializeField] public GhostAppearSoundData[] AppearSounds { get; private set; }
        [field: SerializeField] public GhostAppearSoundData[] SingingSounds { get; private set; }
        [field: SerializeField] public GhostAppearSoundData[] DisappearanceSounds { get; private set; }
    }
}