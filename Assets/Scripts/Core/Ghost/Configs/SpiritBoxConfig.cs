using System;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [Serializable]
    public class SpiritBoxDataElement
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }

        public static SpiritBoxDataElement Empty()
        {
            return new SpiritBoxDataElement("NO RESPONSE", null);
        }

        public SpiritBoxDataElement(string text, AudioClip clip)
        {
            Text = text;
            Clip = clip;
        }
    }

    [CreateAssetMenu(fileName = "RadioConfig", menuName = "Ghosts configs/Radio Config")]
    public class SpiritBoxConfig : ScriptableObject
    {
        [field: SerializeField] public SpiritBoxDataElement[] CloseDistanceVariants { get; private set; } = {};
        [field: Space]
        [field: SerializeField] public SpiritBoxDataElement True { get; private set; }
        [field: SerializeField] public SpiritBoxDataElement Maybe { get; private set; }
        [field: SerializeField] public SpiritBoxDataElement False { get; private set; }
        [field: Space]
        [field: SerializeField] public SpiritBoxDataElement Young { get; private set; }
        [field: SerializeField] public SpiritBoxDataElement Old { get; private set; }
    }
}