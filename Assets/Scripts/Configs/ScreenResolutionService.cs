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
            
            _resolutions = Screen.resolutions.Select(x => new Vector2Int(x.width, x.height)).
                Distinct().ToList();

            if (_configService.Resolution < 0 || _configService.Resolution >= _resolutions.Count)
            {
                _configService.Resolution = _resolutions.Count - 1;
            }

        }

        public void SetResolutionById(int id)
        {
            Screen.SetResolution(_resolutions[id].x, _resolutions[id].y, _configService.IsFullScreen);
        }
    }
}