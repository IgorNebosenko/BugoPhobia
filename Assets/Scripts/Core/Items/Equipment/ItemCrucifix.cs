using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemCrucifix : ItemInstanceBase, IStartHuntInteractable
    {
        [SerializeField] private int countUses = 2;

        public int CountUsesRemain => countUses;
        
        public override void OnMainInteraction()
        {
        }

        public override void OnAlternativeInteraction()
        {
        }

        public bool OnHuntInteraction()
        {
            //ToDo make sfx & vfx
            
            if (countUses > 0)
            {
                --countUses;
                return true;
            }

            return false;
        }
    }
}