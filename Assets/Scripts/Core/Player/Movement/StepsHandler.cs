using ElectrumGames.Audio.Pool;
using ElectrumGames.Audio.Steps;
using ElectrumGames.Configs;
using UnityEngine;

namespace ElectrumGames.Core.Player.Movement
{
    public class StepsHandler : IStepsHandler
    {
        private readonly PlayerConfig _playerConfig;
        private readonly SoundsConfig _soundsConfig;
        private readonly AudioSourcesPool _sourcesPool;
        private readonly SurfaceSoundsList _soundsList;
        
        public StepsHandler(PlayerConfig playerConfig, SoundsConfig soundsConfig, AudioSourcesPool sourcesPool,
            SurfaceSoundsList soundsList)
        {
            _playerConfig = playerConfig;
            _soundsConfig = soundsConfig;
            _sourcesPool = sourcesPool;
            _soundsList = soundsList;
        }
        
        public void FixedSimulate(IInput input, float time)
        {
        }
    }
}