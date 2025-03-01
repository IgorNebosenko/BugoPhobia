using System;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [Serializable]
    public class RadioDataElement
    {
        [field: SerializeField] public string text;
        [field: SerializeField] public AudioClip clip;
    }

    [CreateAssetMenu(fileName = "RadioConfig", menuName = "Ghosts configs/Radio Config")]
    public class RadioConfig : ScriptableObject
    {
        [field: SerializeField] public float PercentChanceResponse { get; private set; }
        [field: Space]
        [field: SerializeField] public RadioDataElement[] CloseDistanceVariants { get; private set; } = {};
        [field: Space]
        [field: SerializeField] public RadioDataElement True { get; private set; }
        [field: SerializeField] public RadioDataElement Maybe { get; private set; }
        [field: SerializeField] public RadioDataElement False { get; private set; }
        [field: Space]
        [field: SerializeField] public RadioDataElement Young { get; private set; }
        [field: SerializeField] public RadioDataElement Old { get; private set; }
    }
}