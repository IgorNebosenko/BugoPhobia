using System.Linq;
using ElectrumGames.Audio.Pool;
using ElectrumGames.Audio.Steps;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost
{
    public class GhostStepsHandler : MonoBehaviour
    {
        private SoundsConfig _soundsConfig;
        private AudioSourcesPool _audioSourcesPool;
        private SurfaceSoundsList _surfaceSoundsList;

        private float _distance;
        
        public void Init(SoundsConfig soundsConfig, AudioSourcesPool audioSourcesPool, SurfaceSoundsList surfaceSoundsList)
        {
            _soundsConfig = soundsConfig;
            _audioSourcesPool = audioSourcesPool;
            _surfaceSoundsList = surfaceSoundsList;
        }

        public void Simulate(float speed, float time)
        {
            _distance += speed * time;

            if (_distance >= _soundsConfig.FrequencyStepsGhost)
            {
                _distance = 0f;
                
                var ray = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(ray, out var hit, float.PositiveInfinity))
                {
                    
                    if (hit.collider.TryGetComponent<SurfaceSoundDefiner>(out var definition))
                    {
                        var clip = _surfaceSoundsList.SurfaceSounds.
                            Where(x => x.SurfaceType == definition.SurfaceType).PickRandom();
                        
                        _audioSourcesPool.Spawn(definition.transform.position, clip.AudioClips.PickRandom());
                    }
                }
            }
        }
    }
}