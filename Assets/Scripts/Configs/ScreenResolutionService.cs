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
        
        public ScreenResolutionService(ConfigService configService)
        {
            _configService = configService;
            
            _resolutions = Screen.resolutions.Select(x => new Vector2Int(x.width, x.height)).
                Distinct().ToList();

            if (!_resolutions.Contains(_configService.Resolution))
            {
                _configService.Resolution = _resolutions[0];
            }

        }

        public void SetResolutionById(int id)
        {
            Screen.SetResolution(_resolutions[id].x, _resolutions[id].y, _configService.IsFullScreen);
        }
    }
}