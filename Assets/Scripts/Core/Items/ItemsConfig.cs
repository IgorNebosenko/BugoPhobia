using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    [Serializable]
    public class ItemConfigData
    {
        [field: SerializeField] public ItemInstanceBase ItemInstance { get; private set; }
        [field: SerializeField] public float ItemMass { get; private set; }
    }

    [CreateAssetMenu(fileName = "Items Config", menuName = "Items/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [field: SerializeField] public List<ItemConfigData> ItemsList { get; private set; }

        public ItemConfigData GetItemByType(ItemType itemType)
        {
            return ItemsList.Find(x => x.ItemInstance.ItemType == itemType);
        }
    }
}