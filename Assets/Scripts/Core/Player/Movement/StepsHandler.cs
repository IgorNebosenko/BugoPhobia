using System.Linq;
using ElectrumGames.Audio.Pool;
using ElectrumGames.Audio.Steps;
using ElectrumGames.Configs;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Player.Movement
{
    public class StepsHandler : IStepsHandler
    {
        private readonly PlayerConfig _playerConfig;
        private readonly SoundsConfig _soundsConfig;
        private readonly AudioSourcesPool _sourcesPool;
        private readonly SurfaceSoundsList _soundsList;
        private readonly IMotor _motor;
        private readonly Transform _transform;

        private float _distance;
        
        public StepsHandler(PlayerConfig playerConfig, SoundsConfig soundsConfig, AudioSourcesPool sourcesPool,
            SurfaceSoundsList soundsList, IMotor motor, Transform transform)
        {
            _playerConfig = playerConfig;
            _soundsConfig = soundsConfig;
            _sourcesPool = sourcesPool;
            _soundsList = soundsList;
            _motor = motor;
            _transform = transform;
        }
        
        public void FixedSimulate(IInput input, float time)
        {
            if (input.Movement == Vector2.zero)
                return;

            if (_motor.CanSprint && input.Sprint)
            {
                _distance += _playerConfig.RunSpeed * time;
            }
            else
            {
                _distance += _playerConfig.DefaultSpeed * time;
            }

            if (_distance >= _soundsConfig.FrequencyStepsPlayer)
            {
                _distance = 0f;
                
                var ray = new Ray(_transform.position, Vector3.down);

                if (Physics.Raycast(ray, out var hit, float.PositiveInfinity))
                {
                    
                    if (hit.collider.TryGetComponent<SurfaceSoundDefiner>(out var definition))
                    {
                        var clip = _soundsList.SurfaceSounds.
                            Where(x => x.SurfaceType == definition.SurfaceType).PickRandom();
                        
                        _sourcesPool.Spawn(definition.transform.position, clip.AudioClips.PickRandom(), 
                            _soundsConfig.DefaultPlayerStepVolume);
                    }
                }
            }
        }
    }
}