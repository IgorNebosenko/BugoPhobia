using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemNotebook : ItemInstanceBase
    {
        [SerializeField] private GameObject[] pictures;

        private bool _isPictureShows;
        
        public override void OnMainInteraction()
        {
        }

        public override void OnAlternativeInteraction()
        {
        }

        public void ShowRandomPicture()
        {
            if (_isPictureShows)
                return;

            _isPictureShows = true;
            
            pictures.PickRandom().SetActive(true);
        }
    }
}