using ElectrumGames.Core.Items;
using ElectrumGames.GlobalEnums;
using TMPro;
using UnityEngine;

namespace ElectrumGames.UI.Boards
{
    public class EquipmentItemComponent : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        [Space]
        [SerializeField] private TMP_Text equipmentName;
        [SerializeField] private TMP_Text equipmentCount;
        [SerializeField] private string countPatent = "{0}/{1}";
        [Space]
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color emptyColor;

        public void OnInit(ItemsConfig itemsConfig, ItemType selectedType, int selectedCount, int maxCount)
        {
            itemType = selectedType;

            equipmentName.text = itemsConfig.GetItemByType(itemType).ItemLocalizedName;
            equipmentCount.text = string.Format(countPatent, selectedCount, maxCount);

            equipmentName.color = selectedCount != 0 ? defaultColor : emptyColor;
            equipmentCount.color = selectedCount != 0 ? defaultColor : emptyColor;
        }
    }
}
