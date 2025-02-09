using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemSanityRestorer : ItemInstanceBase
    {
        [SerializeField] private float restoreSanity;

        public override void OnMainInteraction()
        {
            Destroy(gameObject);
        }

        public override void OnAlternativeInteraction()
        {
        }
    }
}