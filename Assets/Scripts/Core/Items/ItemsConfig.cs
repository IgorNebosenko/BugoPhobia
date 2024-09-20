using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    [Serializable]
    public class ItemConfigData
    {
        [field: SerializeField] public ItemInstanceBase ItemInstance { get; private set; }
        [field: SerializeField] public Vector3 UserPositionAtCamera { get; private set; }
        [field: SerializeField] public Quaternion UserRotationAtCamera { get; private set; }
        [field: SerializeField] public string ItemLocalizedName { get; private set; }
        
        [field: SerializeField] public int RequiredLevel { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
    }

    [CreateAssetMenu(fileName = "Items Config", menuName = "Items/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [field: SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 5)] 
        public List<ItemConfigData> ItemsList { get; private set; }

        public ItemConfigData GetItemByType(ItemType itemType)
        {
            return ItemsList.Find(x => x.ItemInstance.ItemType == itemType);
        }
    }
}