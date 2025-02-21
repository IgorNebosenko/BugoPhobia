using System;
using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Audio.Steps
{
    [Serializable]
    public class SurfaceSounds
    {
        [field: SerializeField] public SurfaceType SurfaceType { get; private set; }
        [SerializeField] private List<AudioClip> audioClips;

        public IReadOnlyList<AudioClip> AudioClips => audioClips;
    }

    [CreateAssetMenu(fileName = "FlatSoundsList", menuName = "Audio Config/FlatSoundsList")]
    public class SurfaceSoundsList : ScriptableObject
    {
        [SerializeField] private List<SurfaceSounds> surfaceSounds;

        public IReadOnlyList<SurfaceSounds> SurfaceSounds => surfaceSounds;
    }
}