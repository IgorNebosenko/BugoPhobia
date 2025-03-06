using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    [Serializable]
    public class ItemThrowSoundItem
    {
        [field: SerializeField] public CollisionThrowSound CollisionThrowSound { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }

    [CreateAssetMenu(fileName = "Item Throw Sound Config", menuName = "Items/ItemThrowSoundConfig")]
    public class ItemThrowSoundConfig : ScriptableObject
    {
        [field: SerializeField] public List<ItemThrowSoundItem> ItemThrowSoundItems { get; private set; }
    }
}