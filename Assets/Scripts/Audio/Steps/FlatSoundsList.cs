using System;
using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Audio.Steps
{
    [Serializable]
    public class FlatSounds
    {
        [field: SerializeField] public FlatType FlatType { get; private set; }
        [SerializeField] private List<AudioClip> audioClips;

        public IReadOnlyList<AudioClip> AudioClips => audioClips;
    }

    [CreateAssetMenu(fileName = "FlatSoundsList", menuName = "Audio Config/FlatSoundsList")]
    public class FlatSoundsList : ScriptableObject
    {
        [SerializeField] private List<FlatSounds> flatSounds;

        public IReadOnlyList<FlatSounds> FlatSounds => flatSounds;
    }
}