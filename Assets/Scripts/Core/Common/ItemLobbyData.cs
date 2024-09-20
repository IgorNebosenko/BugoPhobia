using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Common
{
    public class ItemLobbyData
    {
        public ItemType itemType;
        public int currentCount;
        public int maxCount;

        public ItemLobbyData(ItemType itemType, int currentCount, int maxCount)
        {
            this.itemType = itemType;
            this.currentCount = currentCount;
            this.maxCount = maxCount;
        }
    }
}