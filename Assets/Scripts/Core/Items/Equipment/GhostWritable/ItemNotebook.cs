using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Items.GhostWritable
{
    public class ItemNotebook : GhostWritableBase
    {
        [SerializeField] private GameObject[] pictures;

        [SerializeField, Range(0f, 1f)] 
        private float chanceToWrite = 0.5f;

        private bool _isPictureShows;

        public override float ChanceWrite => chanceToWrite;
        
        public override void OnMainInteraction()
        {
        }

        public override void OnAlternativeInteraction()
        {
        }

        public override void Write()
        {
            if (Random.Range(0f, 1f) < chanceToWrite)
                ShowRandomPicture();
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