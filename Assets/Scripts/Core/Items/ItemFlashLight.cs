using DG.Tweening;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemFlashLight : ItemInstanceBase, IGhostHuntingInteractable
    {
        [SerializeField] private Light lightSource;
        [SerializeField] private float flickerDuration = 0.75f;
        [SerializeField] private float flickerSpeed = 0.1f;

        public bool IsOn { get; private set; }

        private Tween _flickerProcess;

        public bool IsGhostHuntInteractable => true;
        
        public override void OnMainInteraction()
        {
            IsOn = !IsOn;

            lightSource.enabled = IsOn;
        }

        public override void OnAlternativeInteraction()
        {
        }

        public bool OnGhostHuntInteractionEnter()
        {
            if (!IsGhostHuntInteractable)
                return false;
            
            _flickerProcess = lightSource.DOIntensity(0f, flickerSpeed)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Flash);
            
            return true;
        }

        public void OnGhostHuntInteractionExit()
        {
            _flickerProcess.Kill();
            
            //TODO check is need return to start
        }
    }
}