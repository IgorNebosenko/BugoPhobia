using System;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    [Serializable]
    public class ItemConfigData
    {
        [field: SerializeField] public ItemInstance ItemInstance { get; private set; }
        [field: SerializeField] public float ItemMass { get; private set; }
    }

    [CreateAssetMenu(fileName = "Items Config", menuName = "Items/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        
    }
}