using System.Collections.Generic;
using ElectrumGames.Audio.Steps;
using ElectrumGames.Extensions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Audio.Pool
{
    public class AudioSourcesPool : MonoBehaviour
    {
        [SerializeField] private AudioSourceHandler sourceHandlerTemplate;
        
        private List<AudioSourceHandler> _audioSourceHandlers = new ();

        private SoundsConfig _soundsConfig;

        [Inject]
        private void Construct(SoundsConfig soundsConfig)
        {
            _soundsConfig = soundsConfig;

            for (var i = 0; i < _soundsConfig.DefaultPoolAudioSourcesSize; i++)
            {
                _audioSourceHandlers.Add(CreateNewHandler());
            }
        }

        private AudioSourceHandler CreateNewHandler()
        {
            var handler = Instantiate(sourceHandlerTemplate, transform);
            handler.gameObject.SetActive(false);
            return handler;
        }

        public AudioSourceHandler Spawn(Vector3 position, AudioClip clip)
        {
            AudioSourceHandler handler;
            
            if (_audioSourceHandlers.Count > 0)
            {
                handler = _audioSourceHandlers.PickRandom();
                _audioSourceHandlers.Remove(handler);
                handler.gameObject.SetActive(true);
                
                handler.Play(position, this, clip);
            }
            else
            {
                handler = CreateNewHandler();
                handler.gameObject.SetActive(true);
                
                handler.Play(position, this, clip);
            }

            return handler;
        }

        public void Despawn(AudioSourceHandler handler)
        {
            handler.transform.parent = transform;
            handler.transform.position = Vector3.zero;
            handler.gameObject.SetActive(false);
        }
    }
}