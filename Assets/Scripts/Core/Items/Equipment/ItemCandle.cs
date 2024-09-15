using System.Linq;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemCandle : ItemInstanceBase
    {
        [SerializeField] private Light lightSource;
        
        public override void OnMainInteraction()
        {
            if (lightSource.enabled)
                return;
            
            var ignitionItems = 
                inventoryReference.Items.Where(x => 
                    x != null && x.ItemType is ItemType.Matches or ItemType.LighterSimple or ItemType.LighterAdvanced).ToArray();

            for (var i = 0; i < ignitionItems.Length; i++)
            {
                if (ignitionItems[i] is ItemMatches matches)
                {
                    if (matches.TryUseMatch())
                    {
                        lightSource.enabled = true;
                        return;
                    }
                }
            }

        }

        public override void OnAlternativeInteraction()
        {
            lightSource.enabled = false;
        }

        public override void OnAfterDrop()
        {
            lightSource.enabled = false;
        }

        private void OnDisable()
        {
            lightSource.enabled = false;
        }
    }
}