using System;
using UniRx;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemMatches : ItemInstanceBase
    {
        [SerializeField] private GameObject matchObject;
        [SerializeField] private Light lightSource;

        [field: SerializeField] public int MatchesCount { get; private set; } = 10;
        [SerializeField] private float matchBurnDuration = 15f;

        private IDisposable lightProcess;
        
        public override void OnMainInteraction()
        {
            ExtinguishMatch();
            
            if (MatchesCount <= 0)
                return;

            --MatchesCount;
            
            matchObject.SetActive(true);
            lightSource.enabled = true;

            lightProcess = Observable.Timer(TimeSpan.FromSeconds(matchBurnDuration))
                .Subscribe(ExtinguishMatch).AddTo(this);
        }
        
        public void ExtinguishMatch(long _ = -1)
        {
            lightProcess?.Dispose();
            
            matchObject.SetActive(false);
            lightSource.enabled = false;
        }

        public override void OnAlternativeInteraction()
        {
            ExtinguishMatch();
        }

        public override void OnAfterDrop()
        {
            ExtinguishMatch();
        }

        private void OnDisable()
        {
            ExtinguishMatch();
        }
    }
}