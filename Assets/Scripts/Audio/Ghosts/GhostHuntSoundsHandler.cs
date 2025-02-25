using System;
using System.Linq;
using ElectrumGames.Extensions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Audio.Ghosts
{
    public class GhostHuntSoundsHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private GhostHuntingSounds _ghostHuntingSounds;
        private bool _isMale;
        
        private GhostAppearSounds _ghostAppearSounds;
        private GhostHuntingSoundElement _soundElement;
        
        public event Action PlayFinished;
        private bool _lastPlayState;
        
        public void Construct(GhostHuntingSounds ghostHuntingSounds, GhostAppearSounds ghostAppearSounds)
        {
            _ghostHuntingSounds = ghostHuntingSounds;
            _ghostAppearSounds = ghostAppearSounds;
        }

        public void Init(bool isMale)
        {
            _isMale = isMale;
            _soundElement = _ghostHuntingSounds.Elements.Where(x => isMale ? x.CanUseMale : x.CanUseFemale)
                .PickRandom();
        }

        private void Update()
        {
            if (!audioSource.isPlaying && _lastPlayState)
                PlayFinished?.Invoke();

            _lastPlayState = audioSource.isPlaying;
        }

        public void PlayRandomAppearSound()
        {
            audioSource.Stop();

            var audioData =_ghostAppearSounds.AppearSounds.Where(x 
                    => _isMale ? x.CanUseMale : x.CanUseFemale).PickRandom();

            audioSource.PlayOneShot(audioData.Clip);
        }
        
        public void PlayRandomSingingSound()
        {
            audioSource.Stop();

            var audioData =_ghostAppearSounds.SingingSounds.Where(x 
                => _isMale ? x.CanUseMale : x.CanUseFemale).PickRandom();

            audioSource.PlayOneShot(audioData.Clip);
        }
        
        public void PlayRandomDisappearSound()
        {
            audioSource.Stop();

            var audioData =_ghostAppearSounds.DisappearanceSounds.Where(x 
                => _isMale ? x.CanUseMale : x.CanUseFemale).PickRandom();

            audioSource.PlayOneShot(audioData.Clip);
        }
    }
}