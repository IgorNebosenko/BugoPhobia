using System;
using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Items;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    [Serializable]
    public class LobbyItemData
    {
        public ItemType itemType;
        public int itemsCount;

        public LobbyItemData(ItemType itemType, int itemsCount)
        {
            this.itemType = itemType;
            this.itemsCount = itemsCount;
        }
    }

    public class LobbyItemsHandler
    {
        private const string InventoryKey = "Inventory";

        private readonly ItemsConfig _itemsConfig;
        
        private List<LobbyItemData> _lobbyItems = new ();
        
        public LobbyItemsHandler(ItemsConfig itemsConfig)
        {
            _itemsConfig = itemsConfig;
            ReadInventory();
        }

        private void ReadInventory()
        {
            _lobbyItems = JsonUtility.FromJson<List<LobbyItemData>>(PlayerPrefsAes.GetEncryptedString(InventoryKey, "{}"));
        }

        public bool TryUseItem(ItemType type)
        {
            var item = _lobbyItems.FirstOrDefault(x => x.itemType == type);

            if (item == null || item.itemsCount <= 0)
                return false;

            item.itemsCount--;
            SaveInventory();
            return true;
        }

        public void AddItem(ItemType type)
        {
            var item = _lobbyItems.FirstOrDefault(x => x.itemType == type);

            if (item == null)
            {
                _lobbyItems.Add(new LobbyItemData(type, 1));
            }
            else
            {
                item.itemsCount++;
            }
            
            SaveInventory();
        }

        public List<ItemLobbyData> GetSortedList()
        {
            var result = new List<ItemLobbyData>();

            var sublist = _lobbyItems.OrderBy(x => x.itemType).ToList();

            for (var i = 0; i < sublist.Count; i++)
            {
                var maxCount = _itemsConfig.GetItemByType(sublist[i].itemType).MaxCountOnMission;
                result.Add(new ItemLobbyData(sublist[i].itemType, sublist[i].itemsCount, maxCount));
            }

            return result;
        }

        private void SaveInventory()
        {
            PlayerPrefsAes.SetEncryptedString(InventoryKey, JsonUtility.ToJson(_lobbyItems));
        }
    }
}