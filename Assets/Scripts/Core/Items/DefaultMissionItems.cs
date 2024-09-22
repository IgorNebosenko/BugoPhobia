using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    [CreateAssetMenu(fileName = "DefaultMissionItemsConfig", menuName = "Items/DefaultMissionItems")]
    public class DefaultMissionItems : ScriptableObject
    {
        [SerializeField] private ItemType[] items;

        public IReadOnlyList<ItemType> Items => items;
    }
}