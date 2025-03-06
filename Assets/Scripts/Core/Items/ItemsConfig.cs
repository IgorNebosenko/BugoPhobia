using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    [Serializable]
    public class ItemConfigData : EnvironmentItemConfig
    {
        [field: SerializeField] public string ItemLocalizedName { get; private set; }
        
        [field: SerializeField] public int RequiredLevel { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public int MaxCountOnMission { get; private set; }
    }

    [Serializable]
    public class EnvironmentItemConfig
    {
        [field: SerializeField] public ItemInstanceBase ItemInstance { get; private set; }
        [field: SerializeField] public Vector3 UserPositionAtCamera { get; private set; }
        [field: SerializeField] public Quaternion UserRotationAtCamera { get; private set; }
    }

    [CreateAssetMenu(fileName = "Items Config", menuName = "Items/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [field: SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 5)] 
        public List<ItemConfigData> ItemsList { get; private set; }
        
        [field: SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 5)] 
        public List<EnvironmentItemConfig> EnvironmentItemsList { get; private set; }

        public ItemConfigData GetItemByType(ItemType itemType)
        {
            if ((int) itemType >= 1000)
            {
                Debug.LogError($"Try to get item but index is in range of environment items! {itemType}");
                return null;
            }
            
            return ItemsList.Find(x => x.ItemInstance.ItemType == itemType);
        }

        public EnvironmentItemConfig GetEnvironmentItemByType(ItemType itemType)
        {
            if ((int) itemType < 1000)
            {
                Debug.LogError($"Try to get item but index is in range of equipment items! {itemType}");
                return null;
            }

            return EnvironmentItemsList.Find(x => x.ItemInstance.ItemType == itemType);
        }
    }
}