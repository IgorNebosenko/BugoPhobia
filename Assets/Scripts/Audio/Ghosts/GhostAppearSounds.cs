using UnityEngine;

namespace ElectrumGames.Audio.Ghosts
{
    [CreateAssetMenu(fileName = "GhostAppearSounds", menuName = "Audio Config/Ghost Appear Sounds")]
    public class GhostAppearSounds : ScriptableObject
    {
        [field: SerializeField] public AudioClip[] AppearSounds { get; private set; }
        [field: SerializeField] public AudioClip[] SingingSounds { get; private set; }
    }
}