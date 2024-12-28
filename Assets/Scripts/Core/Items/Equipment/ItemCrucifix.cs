using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemCrucifix : ItemInstanceBase, IStartHuntInteractable
    {
        [SerializeField] private int countUses = 2;
        [Space]
        [SerializeField] private AudioSource firstUseSource;
        [SerializeField] private AudioSource secondUseSource;

        public int CountUsesRemain => countUses;
        
        public override void OnMainInteraction()
        {
        }

        public override void OnAlternativeInteraction()
        {
        }

        public bool OnHuntInteraction()
        {
            //ToDo make vfx
            
            if (countUses > 0)
            {
                if (countUses == 2)
                    firstUseSource.Play();
                else
                    secondUseSource.Play();
                
                --countUses;
                return true;
            }

            return false;
        }
    }
}