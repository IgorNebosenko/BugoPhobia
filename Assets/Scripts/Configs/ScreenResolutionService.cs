using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Configs
{
    public class ScreenResolutionService
    {
        private readonly ConfigService _configService;

        private List<Vector2Int> _resolutions;

        public List<string> ResolutionsNames => _resolutions.Select(res => $"{res.x}x{res.y}").ToList();
        public IReadOnlyList<Vector2Int> Resolutions => _resolutions;
        
        public ScreenResolutionService(ConfigService configService)
        {
            _configService = configService;
            
#if UNITY_STANDALONE
            _resolutions = Screen.resolutions.Select(x => new Vector2Int(x.width, x.height)).
                Distinct().ToList();
#elif UNITY_ANDROID
            _resolutions = new List<Vector2Int>()
            {
                new Vector2Int(640, 480),
                new Vector2Int(1280, 720),
                new Vector2Int(1600, 720),
                new Vector2Int(1920, 1080),
                new Vector2Int(2340, 1080)
            };
#endif

            if (_configService.Resolution < 0 || _configService.Resolution >= _resolutions.Count)
            {
#if UNITY_STANDALONE
                _configService.Resolution = _resolutions.Count - 1; //Max res
#elif UNITY_ANDROID
                _configService.Resolution = _resolutions.Count - 2; // 1920x1080
#endif
            }

        }

        public void SetResolutionById(int id)
        {
            Screen.SetResolution(_resolutions[id].x, _resolutions[id].y, _configService.IsFullScreen);
        }
    }
}