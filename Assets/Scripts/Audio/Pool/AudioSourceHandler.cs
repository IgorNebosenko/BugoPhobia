using System.Collections;
using UnityEngine;

namespace ElectrumGames.Audio.Pool
{
    public class AudioSourceHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private AudioSourcesPool _audioSourcesPool;
        

        public void Play(Vector3 position, AudioSourcesPool pool, AudioClip audioClip)
        {
            transform.position = position;
            
            _audioSourcesPool = pool;
            
            audioSource.clip = audioClip;
            audioSource.Play();
            StartCoroutine(CheckIfAudioFinished());
        }
        
        private IEnumerator CheckIfAudioFinished()
        {
            while (audioSource.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }
            
            _audioSourcesPool.Despawn(this);
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}