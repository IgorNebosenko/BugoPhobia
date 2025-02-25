using System;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Audio.Ghosts
{
    public class GhostHuntSoundsHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private GhostHuntingSounds _ghostHuntingSounds;
        private GhostHuntSounds _ghostHuntSounds;
        
        public Action PlayFinished;
        private bool _lastPlayState;

        [Inject]
        private void Construct(GhostHuntingSounds ghostHuntingSounds)
        {
            _ghostHuntingSounds = ghostHuntingSounds;
        }

        public void Init(GhostHuntSounds ghostHuntSounds)
        {
            _ghostHuntSounds = ghostHuntSounds;
        }

        private void Update()
        {
            if (!audioSource.isPlaying && _lastPlayState)
                PlayFinished?.Invoke();

            _lastPlayState = audioSource.isPlaying;
        }
    }
}