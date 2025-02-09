using ElectrumGames.CommonInterfaces;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items
{
    public class ItemSanityRestorer : ItemInstanceBase
    {
        [SerializeField] private float restoreSanity;

        private ICanChangeSanity _sanityHandler;
        
        [Inject]
        private void Construct([Inject(Id = "Host")] ICanChangeSanity sanityHandler)
        {
            _sanityHandler = sanityHandler;
        }
        
        public override void OnMainInteraction()
        {
            if (_sanityHandler.CurrentSanity >= 99f)
                return;
            
            _sanityHandler.ChangeSanity(restoreSanity);
            Destroy(gameObject);
        }

        public override void OnAlternativeInteraction()
        {
        }
    }
}